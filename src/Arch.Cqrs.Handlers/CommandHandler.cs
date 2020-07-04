using Arch.CqrsClient.Command.Customer;
using Arch.Domain;
using Arch.Domain.Core;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Core.Event;
using Arch.Domain.Event;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.Cqrs.Contracts;
using Arch.Infra.Shared.Cqrs.Event;
using Arch.Infra.Shared.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Arch.Cqrs.Handlers
{
    public abstract class CommandHandler<T> where T : Entity
    {
        private readonly ArchCoreContext _architectureContext;
        private readonly EventSourcingCoreContext _eventSourcingContext;
        private readonly IDomainNotification _notifications;
        private readonly IEventRepository _eventRepository;
        private readonly AuthService _authService;

        protected CommandHandler(
            ArchCoreContext architectureContext, IDomainNotification notifications, IEventRepository eventRepository,
            EventSourcingCoreContext eventSourcingContext, AuthService authService)
        {
            _architectureContext = architectureContext;
            _notifications = notifications;
            _eventRepository = eventRepository;
            _eventSourcingContext = eventSourcingContext;
            _authService = authService;
        }

        protected DbSet<T> Db() => _architectureContext.Set<T>();
        protected bool Any(Expression<Func<T, bool>> predicate) =>
            _architectureContext.Set<T>().Any(predicate);

        protected bool ExistsValidation(Expression<Func<T, bool>> predicate, string action, string message)
        {
            if (Any(predicate))
            {
                AddNotification(action, message);
                return true;
            }
            return false;
        }

        protected T GetLast(Guid id) =>
            Db().AsNoTracking().OrderBy(_ => _.CreatedDate).FirstOrDefault(_ => _.Id == id);

        protected void ValidateCommand(CommandAction cmd)
        {
            if (cmd.IsValid()) return;
            foreach (var error in cmd.ValidationResult.Errors)
                AddNotification(new DomainNotification(cmd.Action, error.ErrorMessage));
        }

        protected void Commit(Event evet)
        {
            if (_notifications.HasNotifications()) return;
            if (_architectureContext.SaveChanges() > 0)
            {
                evet.Who = _authService.UserLoggedIn;
                var anterior = _eventRepository.GetLastEvent(evet.AggregateId);
                var objJson = anterior != null
                    ? ReadToObject(((StoredEvent)anterior).Data, ((StoredEvent)anterior).Assembly)
                    : null;
                _eventRepository.Save(evet, objJson);
            }
            else
                AddNotification(new DomainNotification("Commit", "We had a problem during saving your data."));
        }

        public List<MemberInfo> GetAttributs(object obj)
        {
            var tipoGen = typeof(SourceFluent<>).MakeGenericType(obj.GetType());

            var target = obj.GetType().Assembly;
            var assemblies = target.GetReferencedAssemblies()
                .Select(Assembly.Load).ToList();
            assemblies.Add(target);

            var map = assemblies.SelectMany(_ => _.GetExportedTypes())
                .FirstOrDefault(_ => tipoGen.IsAssignableFrom(_));

            var tipo = (dynamic)Activator.CreateInstance(map);
            tipo.Configuration(tipo);

            return (List<MemberInfo>)tipo.Members;
        }

        protected void Commit(Entity entity, ICommand command, string action, object lastEntity = null)
        {
            if (_notifications.HasNotifications()) return;
            if (_architectureContext.SaveChanges() > 0)
            {
                var eventEntity = EventEntity.GetEvent(
                    action, entity, command, _authService.UserLoggedIn, lastEntity);

                _eventSourcingContext.EventEntities.Add(eventEntity);
                var teste = JObject.Parse(eventEntity.Data);
                if (JObject.Parse(eventEntity.Data).HasValues)
                {
                    _eventSourcingContext.SaveChanges();
                }
            }
            else
                AddNotification(new DomainNotification("Commit", "We had a problem during saving your data."));
        }

        protected void Commit(Entity entity, string action, Entity lastEntity = null)
        {
            if (_notifications.HasNotifications()) return;
            if (_architectureContext.SaveChanges() > 0)
            {
                var eventEntity = new EventEntity(action, entity, _authService.UserLoggedIn, lastEntity);

                _eventSourcingContext.Add(eventEntity);
                _eventSourcingContext.SaveChanges();
            }
            else
                AddNotification(new DomainNotification("Commit", "We had a problem during saving your data."));
        }

        protected void AddNotification(DomainNotification notification) =>
            _notifications.Add(notification);

        protected void AddNotification(string action, string message) =>
            this.AddNotification(new DomainNotification(action, message));

        public static object ReadToObject(string json, string typeP)
        {
            var asm = typeof(CreateCustomer).Assembly;
            var type = asm.GetType(typeP);

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var ser = new DataContractJsonSerializer(type);
            var res = ser.ReadObject(ms) as object;
            ms.Close();

            Convert.ChangeType(res, type);
            return res;
        }
    }
}

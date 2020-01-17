using Arch.CqrsClient.Command.Customer;
using Arch.Domain.Core.DomainNotifications;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.Cqrs.Contracts;
using Arch.Infra.Shared.EventSourcing;
using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Arch.CqrsHandlers.Dapper.Customers
{
    public class CustomerCommandHandlerDapper :
        ICommandHandler<CreateCustomer>
    {
        private readonly DapperContext _context;
        private readonly IDomainNotification _notifications;
        private readonly EventSourcingDapperContext _eventSourcingContext;

        public CustomerCommandHandlerDapper(DapperContext context, IDomainNotification notifications)
        {
            _context = context;
            _notifications = notifications;
        }

        public void Handle(CreateCustomer command)

        {
            ValidateCommand(command);
            var exists = false;
            if (EmailExists(command.Email))
            {
                AddNotification(command.Action, "The customer e-mail has already been taken.");
                exists = true;
            }
            if (exists) { return; }

            var idAdress = Guid.NewGuid();
            var idCustomer = Guid.NewGuid();


            if (_notifications.HasNotifications()) return;
            AddAddress(command, idAdress);
            AddCustomer(command, idAdress, idCustomer);

            var action = "Customer Created";
            Commit(idCustomer, command, "Customer Created");
        }

        protected void Commit(Guid aggregateId, ICommand command, string action, object lastEntity = null)

        {

            if (true)
            {
                var eventEntity = EventEntity.GetEvent(action, aggregateId, command, "Marcos", lastEntity);

                if (JObject.Parse(eventEntity.Data).HasValues)
                {
                    AddEvent(command, aggregateId);
                }
            }
            else
                AddNotification(new DomainNotification("Commit", "We had a problem during saving your data."));
        }

        private void AddEvent(ICommand command, Guid aggregateId)
        {
            var json = EventEntity.GetEvent("Customer Created", aggregateId, command, "Marcos");
            var sb = new StringBuilder()
                .AppendLine($"INSERT INTO EventEntities (Id, Action, AggregateId, Assembly, Data, When, Who)" + " VALUES " +
                $"(\"{Guid.NewGuid()}\", \"Customer Created\", \"{aggregateId}\", \"_\" , \"'{json.Data}'\", \"{DateTime.Now}\", \"Marcos\")");
            _eventSourcingContext.Connection.Execute(sb.ToString(), new { });
        }


        private void AddCustomer(CreateCustomer command, Guid idAddress, Guid idCustomer)
        {
            var insertCustomer = new StringBuilder()
                .AppendLine("INSERT INTO CUSTOMERS")
                .AppendLine("(Id, AddressId, BirthDate, EmailAddress, FirstName, LastName, Score, CreatedDate)")
                .AppendLine("VALUES ")
                .AppendLine($"(\"{idCustomer}\", \"{idAddress}\", \"{command.BirthDate}\",  \"{command.Email}\", \"{command.FirstName}\",")
                .AppendLine($"\"{command.LastName}\", {command.Score}, \"{DateTime.Now}\")")
                .ToString();

            _context.Connection.Execute(insertCustomer, new { });

        }
        private void AddAddress(CreateCustomer command, Guid idAddress)
        {
            var insertAddress = new StringBuilder()
                   .AppendLine("INSERT INTO ADDRESSES")
                   .AppendLine("(Id, City, Number, Street, ZipCode, CreatedDate)")
                   .AppendLine("VALUES ")
                   .AppendLine($"(\"{idAddress}\", \"{command.City}\", \"{command.Number}\", \"{command.Street}\", \"{command.ZipCode}\", \"{DateTime.Now}\")").ToString();

            _context.Connection.Execute(insertAddress, new { });
        }

        private bool EmailExists(string email)
        {
            var emailExistsSql = new StringBuilder()
                 .AppendLine("SELECT CASE")
                 .AppendLine("    WHEN EXISTS (")
                 .AppendLine("        SELECT 1")
                 .AppendLine("        FROM Customers AS x")
                 .AppendLine($"        WHERE x.EmailAddress = '{email}')")
                 .AppendLine("    THEN 1 ELSE 0")
                 .AppendLine("END").ToString();
            return _context.Connection.QueryFirst<bool>(emailExistsSql);
        }

        protected void ValidateCommand(CommandAction cmd)
        {
            if (cmd.IsValid()) return;
            foreach (var error in cmd.ValidationResult.Errors)
                AddNotification(new DomainNotification(cmd.Action, error.ErrorMessage));
        }

        private void AddNotification(string action, string message) =>
            AddNotification(new DomainNotification(action, message));

        private void AddNotification(DomainNotification notification) =>
            _notifications.Add(notification);

        //private string SqlCreate(string table, Customer obj)
        //{
        //    var invalids = new[] { "Action", "AggregateId", "Who", "ValidationResult" };
        //    var propsSql = "";
        //    var valuesSql = "";
        //    var properties = obj.GetType().GetProperties();
        //    var lenght = properties.Length;
        //    for (var i = 0; i < lenght; i++)
        //        if (!invalids.Contains(properties[i].Name))
        //        {
        //            if (i == lenght - 4)
        //            {
        //                propsSql += properties[i].Name;
        //                valuesSql += $"@{properties[i].Name}";
        //            }
        //            else
        //            {
        //                propsSql += properties[i].Name + ", ";
        //                valuesSql += $"@{properties[i].Name}, ";
        //            }
        //        }
        //    var insert = $"INSERT INTO {table} ({propsSql}) VALUES ({valuesSql})";

        //    return table;
        //}
    }
}

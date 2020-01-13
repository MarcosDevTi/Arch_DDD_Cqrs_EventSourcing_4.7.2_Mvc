using Arch.Cqrs.Client.Command.Customer;
using Arch.Cqrs.Client.Command.EventSourcing;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Event;
using Arch.Infra.Shared.Cqrs;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class InsertController : BaseController
    {
        private readonly IProcessor _processor;
        public InsertController(IProcessor processor, IDomainNotification notifications, IEventRepository eventRepository)
           : base(notifications)
        {
            _processor = processor;
        }
        // GET: Insert
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertListLotEf500()
        {
            _processor.Send(new InsertVolumeCustomers(500));
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult InsertLot500Dapper()
        {
            _processor.Send(new InsertVolumeCustomersDapper(500));
            return RedirectToAction("Index", "Customer", new { });
        }


        public ActionResult InsertLot5000Dapper()
        {
            _processor.Send(new InsertVolumeCustomersDapper(5000));
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult InsertOpenCloseDapper500()
        {
            _processor.Send(new InsertOpenCloseDapper500(500));
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult InsertListLotEf5000()
        {
            _processor.Send(new InsertVolumeCustomers(5000));
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult TrucateCustomers()
        {
            _processor.Send(new TrucateCustomers());
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult TrucateEventSourcing()
        {
            _processor.Send(new TruncateEventSourcing());
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult InsertOpenClose()
        {
            var faker = new Faker();
            var list = new List<CreateCustomer>();
            for (var i = 0; i < 500; i++)
            {
                var minDate = DateTime.Now.AddYears(-30);
                var maxDate = DateTime.Now.AddYears(-60);

                var createCustomer = new CreateCustomer
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Email = GenerateEmails(faker.Person.Email, i),
                    BirthDate = faker.Date.Between(minDate, maxDate),
                    Street = faker.Address.StreetName(),
                    Number = faker.Address.BuildingNumber(),
                    City = faker.Address.City(),
                    ZipCode = faker.Address.ZipCode()
                };

                list.Add(createCustomer);
            }

            list.ForEach(_ => _processor.Send(_));
            return RedirectToAction("Index");
        }

        private string GenerateEmails(string str, int i)
        {
            return "a" + i + str;
        }
    }
}
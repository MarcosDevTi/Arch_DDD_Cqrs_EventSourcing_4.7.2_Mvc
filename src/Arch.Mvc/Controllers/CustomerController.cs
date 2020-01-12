using Arch.Cqrs.Client.Command.Customer;
using Arch.Cqrs.Client.Command.Customer.Generics;
using Arch.Cqrs.Client.Paging;
using Arch.Cqrs.Client.Query.Customer.Queries;
using Arch.Domain.Core.DomainNotifications;
using Arch.Domain.Event;
using Arch.Infra.Shared.Cqrs;
using Arch.Paging;
using AutoMapper;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IProcessor _processor;
        private readonly IEventRepository _eventRepository;
        private readonly IDomainNotification _notifications;
        public CustomerController(IProcessor processor, IDomainNotification notifications, IEventRepository eventRepository)
            : base(notifications)
        {
            _processor = processor;
            _notifications = notifications;
            _eventRepository = eventRepository;
        }
        public ActionResult Index(Paging.Paging paging)
        {
            var pagedResult = _processor.Get(new GetCustomersPaging(paging));
            return View(pagedResult);
        }

        [HttpPost]
        public ActionResult Create(CreateCustomer createCustomer)
        {
            
            _processor.Send(createCustomer);
            return ViewWithValidation(createCustomer);
        }

        public ActionResult Edit(Guid customerId)
        {
            var customerForEdit = _processor.Get(new GetCustomerForUpdate(customerId));
            return View(customerForEdit);
        }

        [HttpPost]
        public ActionResult Edit(UpdateCustomer updateCustomer)
        {
            _processor.Send(updateCustomer);
            return ViewWithValidation(updateCustomer);
        }

        public ActionResult Delete(Guid id)
        {
            _processor.Send(new DeleteCustomer {Id = id });
            return RedirectToAction("Index");
        }

        [HttpGet, Route("history")]
        public ActionResult History(Guid aggregateId)
        {
            var customersHistory = _processor.Get(new GetCustomerHistory { AggregateId = aggregateId });
            return View(customersHistory);
        }

        public ActionResult History()
        {
            return View(_eventRepository.GetAllHistories());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.HasError = false;
            ViewBag.Errors = new List<string>();

            return View();
        }
    }
}
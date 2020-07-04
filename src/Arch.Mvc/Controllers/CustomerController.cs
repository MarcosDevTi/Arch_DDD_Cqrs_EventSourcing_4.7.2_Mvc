using Arch.Cqrs.Client.Query.Generics;
using Arch.CqrsClient.Command.Customer;
using Arch.CqrsClient.Query.Customer.Queries;
using Arch.Domain;
using Arch.Domain.Core.DomainNotifications;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IProcessor _processor;
        private readonly AuthService _authService;

        public CustomerController(
            IProcessor processor,
            IDomainNotification notifications, AuthService authService)
            : base(notifications)
        {
            _processor = processor;
            _authService = authService;
        }

        public ActionResult Index(Paging paging, string successMessage = null)
        {
            ViewBag.User = _authService.UserLoggedIn;
            ViewBag.MessageSuccess = successMessage;
            return View(_processor.Get(new GetCustomersPaging(paging)));
        }

        public ActionResult UserChange(string userName)
        {
            _authService.UserLoggedIn = userName;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.HasError = false;
            ViewBag.Errors = new List<string>();

            return View();
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
            _processor.Send(new DeleteCustomer { Id = id });
            return RedirectToAction("Index");
        }

        [HttpGet, Route("history")]
        public ActionResult History(Guid aggregateId)
        {
            var customersHistory = _processor.Get(new GetCustomerHistory { AggregateId = aggregateId });
            return View(customersHistory);
        }

        [HttpGet, Route("history-by-users")]
        public ActionResult HistoryByUsers(string userId)
        {
            var result = _processor.Get(new GetEntityByUserId { UserId = userId });
            return View(result);
        }
    }
}
using Arch.Domain.Core.DomainNotifications;
using Arch.Infra.Shared.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class BaseController: Controller
    {
        private readonly IDomainNotification _notifications;

        public BaseController(IDomainNotification notifications)
        {
            _notifications = notifications;
        }

        public bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        public ActionResult ViewWithValidation(Command command)
        {
            ViewBag.Errors = _notifications.GetNotifications().Select(_ => _.Value).ToList();
            ViewBag.HasError = _notifications.HasNotifications();
            if (_notifications.HasNotifications())
                return View(command);
            return RedirectToAction("Index");
        }

       

    }
}
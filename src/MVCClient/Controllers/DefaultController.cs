using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Messages;
using Microsoft.AspNet.SignalR;
using NServiceBus;
using ReadModel;

namespace MVCClient.Controllers
{

    public class CustomerHub : Hub, IHandleMessages<CustomerDeleted>
    {
        public void Handle(CustomerDeleted message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.customerDeleted(message.Id);
        }
    }

    public class DefaultController : Controller
    {

        public ActionResult Index()
        {
            MvcApplication.Bus.Send(new CreateCustomer(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            var context = new CQRSExampleEntities();
            ViewBag.Customers = (from x in context.Customers select x).ToList();

            return View();
        }

        public ActionResult Blacklist(Guid id)
        {
            MvcApplication.Bus.Send(new BlackListEmailAddress(id));
            return RedirectToAction("Index");
        }

        public ActionResult Unblacklist(Guid id)
        {
            MvcApplication.Bus.Send(new UnblacklistEmailAddress(id));
            return RedirectToAction("Index");
        }

        public void Delete(Guid id)
        {
            MvcApplication.Bus.Send(new DeleteCustomer(id));
        }
    }
}

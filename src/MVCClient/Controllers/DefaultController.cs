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

    public class CustomerHub : Hub, IHandleMessages<CustomerDeleted>, IHandleMessages<CustomerCreated>, IHandleMessages<CustomerEmailBlacklisted>, IHandleMessages<CustomerEmailUnblacklisted>
    {
        public void Handle(CustomerDeleted message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.customerDeleted(message.Id);
        }

        public void Handle(CustomerCreated message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.customerCreated(message.Id, message.Name, message.EmailAddress);
        }

        public void Handle(CustomerEmailBlacklisted message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.customerBlacklisted(message.Id);
        }

        public void Handle(CustomerEmailUnblacklisted message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.customerUnblacklisted(message.Id);
        }
    }

    public class DefaultController : Controller
    {

        public ActionResult Index()
        {
            var context = new CQRSExampleEntities();
            ViewBag.ActiveCustomers = (from x in context.Customers where x.IsBlacklisted==false select x).ToList();
            ViewBag.BlacklistedCustomers = (from x in context.Customers where x.IsBlacklisted == true select x).ToList();

            return View();
        }

        public void Blacklist(Guid id)
        {
            MvcApplication.Bus.Send(new BlackListEmailAddress(id));
        }

        public void Unblacklist(Guid id)
        {
            MvcApplication.Bus.Send(new UnblacklistEmailAddress(id));
        }

        public void Delete(Guid id)
        {
            MvcApplication.Bus.Send(new DeleteCustomer(id));
        }

        public void Create()
        {
            var customerId = new Guid(Request["Id"]);
            var customerName = Request["Name"];
            var email = Request["Email"];
                
            MvcApplication.Bus.Send(new CreateCustomer(customerId, customerName, email));
        }
    }
}

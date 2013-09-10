using System.Data.Entity.Infrastructure;
using System.Linq;
using Messages;
using NServiceBus;
using ReadModel;

namespace EventServer
{
    public class CustomerCreatedHandler : IHandleMessages<CustomerCreated>
    {
        private readonly CQRSExampleEntities _context;

        public CustomerCreatedHandler(CQRSExampleEntities context)
        {
            _context = context;
        }

        public void Handle(CustomerCreated message)
        {
            var customer = new Customer
                               {
                                   Id = message.Id, 
                                   Name = message.Name, 
                                   EmailAddress = message.EmailAddress
                               };

            _context.Customers.Add(customer);
            _context.SaveChanges();         
        }
    }

    public class CustomerNameChangedHandler : IHandleMessages<CustomerNameChanged>
    {
        private readonly CQRSExampleEntities _context;

        public CustomerNameChangedHandler(CQRSExampleEntities context)
        {
            _context = context;
        }

        public void Handle(CustomerNameChanged message)
        {
            var customer = (from x in _context.Customers where x.Id == message.Id select x).Single();
            customer.Name = message.Name;

            _context.SaveChanges();
        }
    }

    public class CustomerEmailAddressChangedHandler : IHandleMessages<CustomerEmailAddressChanged>
    {
        private readonly CQRSExampleEntities _context;

        public CustomerEmailAddressChangedHandler(CQRSExampleEntities context)
        {
            _context = context;
        }

        public void Handle(CustomerEmailAddressChanged message)
        {
            var customer = (from x in _context.Customers where x.Id == message.Id select x).Single();
            customer.EmailAddress = message.EmailAddress;

            _context.SaveChanges();
        }
    }

    public class CustomerEmailBlacklistedHandler : IHandleMessages<CustomerEmailBlacklisted>
    {
        private readonly CQRSExampleEntities _context;

        public CustomerEmailBlacklistedHandler(CQRSExampleEntities context)
        {
            _context = context;
        }

        public void Handle(CustomerEmailBlacklisted message)
        {
            var customer = (from x in _context.Customers where x.Id == message.Id select x).Single();
            customer.IsBlacklisted = true;

            _context.SaveChanges();
        }
    }

    public class CustomerEmailUnblacklistedHandler : IHandleMessages<CustomerEmailUnblacklisted>
    {
        private readonly CQRSExampleEntities _context;

        public CustomerEmailUnblacklistedHandler(CQRSExampleEntities context)
        {
            _context = context;
        }

        public void Handle(CustomerEmailUnblacklisted message)
        {
            var customer = (from x in _context.Customers where x.Id == message.Id select x).Single();
            customer.IsBlacklisted = false;

            _context.SaveChanges();
        }
    }

    public class CustomerDeletedHandler : IHandleMessages<CustomerDeleted>
    {
        private readonly CQRSExampleEntities _context;

        public CustomerDeletedHandler(CQRSExampleEntities context)
        {
            _context = context;
        }

        public void Handle(CustomerDeleted message)
        {
            var customer = (from x in _context.Customers where x.Id == message.Id select x).Single();
            _context.Customers.Remove(customer);

            _context.SaveChanges();
        }
    }
}
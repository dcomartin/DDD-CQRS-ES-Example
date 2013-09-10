using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Messages
{
    public class CustomerCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public CustomerCreated(Guid id, string name, string emailAddress)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
        }
    }

    public class CustomerNameChanged : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CustomerNameChanged(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class CustomerDeleted : IEvent
    {
        public Guid Id { get; set; }

        public CustomerDeleted(Guid id)
        {
            Id = id;
        }
    }

    public class CustomerEmailAddressChanged : IEvent
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }

        public CustomerEmailAddressChanged(Guid id, string emailAddress)
        {
            Id = id;
            EmailAddress = emailAddress;
        }
    }

    public class CustomerEmailBlacklisted : IEvent
    {
        public Guid Id { get; set; }

        public CustomerEmailBlacklisted(Guid id)
        {
            Id = id;
        }
    }

    public class CustomerEmailUnblacklisted : IEvent
    {
        public Guid Id { get; set; }

        public CustomerEmailUnblacklisted(Guid id)
        {
            Id = id;
        }
    }

}

using System;
using NServiceBus;

namespace Messages
{
    public class CreateCustomer : IMessage
    {
        public string EmailAddress;
        public Guid Id;
        public string Name;

        public CreateCustomer(Guid id, string name, string emailAddress)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
        }
    }

    public class ChangeCustomerName : IMessage
    {
        public Guid Id;
        public string Name;

        public ChangeCustomerName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class DeleteCustomer : IMessage
    {
        public Guid Id;

        public DeleteCustomer(Guid id)
        {
            Id = id;
        }
    }

    public class ChangeCustomerEmailAddress : IMessage
    {
        public string EmailAddress;
        public Guid Id;

        public ChangeCustomerEmailAddress(Guid id, string emailAddress)
        {
            Id = id;
            EmailAddress = emailAddress;
        }
    }

    public class BlackListEmailAddress : IMessage
    {
        public Guid Id;

        public BlackListEmailAddress(Guid id)
        {
            Id = id;
        }
    }

    public class UnblacklistEmailAddress : IMessage
    {
        public Guid Id;

        public UnblacklistEmailAddress(Guid id)
        {
            Id = id;
        }
    }
}
using System;
using System.Globalization;
using CommonDomain.Persistence;
using Domain;
using Messages;
using NServiceBus;

namespace BusServer
{
    public class CreateCustomerHandler : IHandleMessages<CreateCustomer>
    {
        private readonly IRepository _repository;

        public CreateCustomerHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateCustomer message)
        {                       
            var obj = new Customer(Guid.NewGuid(), message.Name, message.EmailAddress);
            _repository.Save(obj, Guid.NewGuid(), d => { });
        }
    }
}

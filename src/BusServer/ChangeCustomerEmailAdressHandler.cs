using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonDomain.Persistence;
using Domain;
using Messages;
using NServiceBus;

namespace BusServer
{
    public class ChangeCustomerEmailAdressHandler : IHandleMessages<ChangeCustomerEmailAddress>
    {
        private readonly IRepository _repository;

        public ChangeCustomerEmailAdressHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ChangeCustomerEmailAddress message)
        {
            var obj = _repository.GetById<Customer>(message.Id);
            obj.ChangeEmailAddress(message.EmailAddress);

            _repository.Save(obj, Guid.NewGuid(), d => { });
        }
    }
}

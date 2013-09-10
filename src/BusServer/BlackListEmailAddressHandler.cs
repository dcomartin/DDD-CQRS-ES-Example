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
    public class BlackListEmailAddressHandlerr : IHandleMessages<BlackListEmailAddress>
    {
        private readonly IRepository _repository;

        public BlackListEmailAddressHandlerr(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(BlackListEmailAddress message)
        {
            var obj = _repository.GetById<Customer>(message.Id);
            obj.Blacklisted();

            _repository.Save(obj, Guid.NewGuid(), d => { });
        }
    }
}

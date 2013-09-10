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
    public class UnblacklistEmailAddressHandler : IHandleMessages<UnblacklistEmailAddress>
    {
        private readonly IRepository _repository;

        public UnblacklistEmailAddressHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UnblacklistEmailAddress message)
        {
            var obj = _repository.GetById<Customer>(message.Id);
            obj.Unblacklisted();

            _repository.Save(obj, Guid.NewGuid(), d => { });
        }
    }
}

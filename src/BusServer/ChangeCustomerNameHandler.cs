using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using CommonDomain.Persistence;
using Domain;
using Messages;
using NServiceBus;

namespace BusServer
{
    public class ChangeCustomerNameHandler : IHandleMessages<ChangeCustomerName>
    {
        private readonly IRepository _repository;
        public IBus Bus { get; set; }

        public ChangeCustomerNameHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ChangeCustomerName message)
        {
            var obj = _repository.GetById<Customer>(message.Id);
            obj.ChangeName(message.Name);
            
            _repository.Save(obj, Guid.NewGuid(), d => { });
        }
    }
}

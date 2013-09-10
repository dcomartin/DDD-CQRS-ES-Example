using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDomain.Persistence;
using Domain;
using Messages;
using NServiceBus;

namespace BusServer
{
    public class DeleteCustomerHandler : IHandleMessages<DeleteCustomer>
    {
        private readonly IRepository _repository;

        public DeleteCustomerHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(DeleteCustomer message)
        {
            var obj = _repository.GetById<Customer>(message.Id);
            obj.Delete();

            _repository.Save(obj, Guid.NewGuid(), d => { });
        }
    }
}

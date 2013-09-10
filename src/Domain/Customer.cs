using System;
using CommonDomain.Core;
using Messages;

namespace Domain
{
    public class Customer : AggregateBase
    {
        private string _name;
        private bool _isDeleted;
        private EmailAddress _emailAddress;

        public Customer(Guid id)
        {
            Id = id;
        }

        public Customer(Guid id, string name, string emailAddress)
        {
            RaiseEvent(new CustomerCreated(id, name, emailAddress));
        }

        public void ChangeName(string newName)
        {
            RaiseEvent(new CustomerNameChanged(Id, newName));
        }

        public void Delete()
        {
            if (_isDeleted) throw new InvalidOperationException("Customer is already deleted.");
            RaiseEvent(new CustomerDeleted(Id));
        }

        public void ChangeEmailAddress(string newEmail)
        {
            RaiseEvent(new CustomerEmailAddressChanged(Id, newEmail));
        }

        public void Blacklisted()
        {
            RaiseEvent(new CustomerEmailBlacklisted(Id));
        }

        public void Unblacklisted()
        {
            RaiseEvent(new CustomerEmailUnblacklisted(Id));
        }

        public void Apply(CustomerCreated @event)
        {
            Id = @event.Id;
            _name = @event.Name;

            _emailAddress = new EmailAddress(@event.EmailAddress);
        }

        public void Apply(CustomerNameChanged @event)
        {
            _name = @event.Name;
        }

        public void Apply(CustomerDeleted @event)
        {
            _isDeleted = true;
        }

        private void Apply(CustomerEmailAddressChanged @event)
        {
            _emailAddress.ChangeEmailAddress(@event.EmailAddress);
        }

        private void Apply(CustomerEmailBlacklisted @event)
        {
            _emailAddress.Blacklist();
        }

        private void Apply(CustomerEmailUnblacklisted @event)
        {
            _emailAddress.Unblacklist();
        }
    }

    public class EmailAddress
    {
        private bool _isBlacklisted;
        private string _email;

        public EmailAddress() { }

        public EmailAddress(string emailAddress)
        {
            ChangeEmailAddress(emailAddress);
        }
        
        public void ChangeEmailAddress(string email)
        {
            if (!IsValidEmail(email)) throw new FormatException("Invalid Email Address.");
            _email = email;
        }

        public void Blacklist()
        {
            if (IsBlacklisted()) throw new InvalidOperationException("Email Address is already blacklisted.");
            _isBlacklisted = true;
        }

        public void Unblacklist()
        {
            if (IsBlacklisted() == false) throw new InvalidOperationException("Email Address is not blacklisted.");
            _isBlacklisted = false;
        }

        public bool IsBlacklisted()
        {
            return _isBlacklisted;
        }

        public static bool IsValidEmail(string email)
        {
            return true;
        }
    }
}
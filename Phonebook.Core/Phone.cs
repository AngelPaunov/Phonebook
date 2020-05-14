using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonebook.Entities
{
    public class Phone : BaseEntity
    {
        public Phone()
        { }
        public Phone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public Phone(uint userId, uint contactId, uint id = 0, string phoneNumber = null) : this(phoneNumber)
        {
            base.Id = id;
            this.UserId = userId;
            this.ContactId = contactId;
        }
        public uint UserId { get; set; }
        public uint ContactId { get; set; }
        public string PhoneNumber { get; set; }
    }
}

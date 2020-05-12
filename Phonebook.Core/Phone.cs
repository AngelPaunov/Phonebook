using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonebook.Entities
{
    public class Phone : BaseEntity
    {
        public Phone()
        {
        }
        public Phone(uint id, int contactId, string _phoneNumber)
        {
            base.Id = id;
            this.ContactId = contactId;
            this.phoneNumber = _phoneNumber;
        }
        public int ContactId { get; set; }
        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                if ( !value.All(c => c >= '0' && c <= '9') || value.Length < 8 || value.Length > 15)
                    return;
                this.phoneNumber = value;
            }
        }

    }
}

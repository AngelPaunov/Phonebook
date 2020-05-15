using Phonebook.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Views.PhoneViews
{
    public class BasePhoneView
    {
        protected IPhoneRepository phoneRepository;
        protected uint userId;
        protected uint contactId;

        public BasePhoneView(IPhoneRepository phoneRepository, uint userId, uint contactId)
        {
            this.phoneRepository = phoneRepository;
            this.userId = userId;
            this.contactId = contactId;
        }
    }
}

using Phonebook.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Views.PhoneViews
{
    public class BasePhoneView
    {
        protected IPhoneRepository phoneRepository;

        public BasePhoneView(IPhoneRepository phoneRepository)
        {
            this.phoneRepository = phoneRepository;
        }
    }
}

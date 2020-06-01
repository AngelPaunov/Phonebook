using Phonebook.Views.ContactViews;
using System;

namespace Phonebook.Views
{
    public class UserView
    {
        private readonly IServiceProvider _serviceProvider;

        public UserView(IServiceProvider serviceProvider) 
        {
            this._serviceProvider = serviceProvider;
        }

        public void Show(uint userId)
        {
            var contactModifyView = (ContactModifyView)_serviceProvider.GetService(typeof(ContactModifyView));
            contactModifyView.Show(userId);
        }
    }
}

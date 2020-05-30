using Phonebook.Entities;
using Phonebook.Views.ContactViews;

namespace Phonebook.Views
{
    public class UserView
    {
        public UserView() 
        {
        }

        public void Show(uint userId)
        {
            var contactModifyView = new ContactModifyView();
            contactModifyView.Show(userId);
        }
    }
}

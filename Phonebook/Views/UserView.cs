using Phonebook.Views.ContactViews;

namespace Phonebook.Views
{
    public class UserView
    {
        public void Show()
        {
            var contactModifyView = new ContactModifyView();
            contactModifyView.Show();
        }
    }
}

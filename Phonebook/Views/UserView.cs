using Phonebook.Views.ContactViews;

namespace Phonebook.Views
{
    public class UserView
    {
        private uint userId;
        public UserView(uint _userId)
        {
            userId = _userId;
        }
        public void Show()
        {
            var contactModifyView = new ContactModifyView(userId);
            contactModifyView.Show();
        }
    }
}

using Phonebook.Entities;
using Phonebook.Views.ContactViews;

namespace Phonebook.Views
{
    public class UserView
    {
        private readonly uint userId;
        private readonly IContactRepository contactRepository;
        private readonly IPhoneRepository phoneRepository;

        public UserView(uint _userId, IContactRepository contactRepository, IPhoneRepository phoneRepository)
        {
            userId = _userId;
            this.contactRepository = contactRepository;
            this.phoneRepository = phoneRepository;
        }
        public void Show()
        {
            var contactModifyView = new ContactModifyView(userId, contactRepository, phoneRepository);
            contactModifyView.Show();
        }
    }
}

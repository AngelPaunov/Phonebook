using Phonebook.Repositories;
using System;
using System.Linq;

namespace Phonebook.Views.ContactViews
{
    public class ReadAllContactsView
    {
        private uint creatorId;
        public ReadAllContactsView(uint _creatorId)
        {
            creatorId = _creatorId;
        }

        public void Show()
        {
            Console.Clear();

            var contactRepository = new ContactRepository();

            foreach (var contact in contactRepository.ReadAllContacts().Where(c=>c.CreatorId==creatorId))
            {
                Console.WriteLine($"ID: {contact.Id}");
                Console.WriteLine($"First Name: {contact.FirstName}");
                Console.WriteLine($"Last Name: {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine();
            }

            Console.ReadKey();
            return;
        }
    }
}

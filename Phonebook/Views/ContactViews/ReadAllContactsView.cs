using Phonebook.Entities;
using System;
using System.Linq;

namespace Phonebook.Views.ContactViews
{
    public class ReadAllContactsView : BaseContactView
    {
        public ReadAllContactsView(IContactRepository contactRepository) : base(contactRepository)
        { }

        public void Show(uint creatorId)
        {
            Console.Clear();

            foreach (var contact in contactRepository.ReadAllContacts().Where(c => c.CreatorId == creatorId))
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

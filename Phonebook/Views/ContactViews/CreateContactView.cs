using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class CreateContactView : BaseContactView
    {
        public CreateContactView(uint creatorId, IContactRepository contactRepository) : base(contactRepository, creatorId)
        { }

        public void Show()
        {
            Console.Clear();

            var contactFromConsole = GetContactFromConsole();
            contactRepository.CreateContact(contactFromConsole);

            Console.WriteLine("Successfuly created contact.");
            Console.ReadKey(true);
        }

        private Contact GetContactFromConsole()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Invalid first name.");
                Console.ReadKey();
                return null;
            }

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid last name.");
                Console.ReadKey();
                return null;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Invalid email.");
                Console.ReadKey();
                return null;
            }

            return new Contact(creatorId, firstName, lastName, email);
        }
    }
}

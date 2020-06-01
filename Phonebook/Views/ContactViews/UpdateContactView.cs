using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class UpdateContactView : BaseContactView
    {
        public UpdateContactView(IContactRepository contactRepository) : base(contactRepository)
        { }

        private uint _creatorId;

        public void Show(uint creatorId)
        {
            Console.Clear();
            _creatorId = creatorId;
            Console.Write("Input contact's id to update: ");
            uint contactInputId = GetIdFromInput();
            if (contactInputId < 1)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var contactFromInput = GetContactById(creatorId, contactInputId);
            if (contactFromInput == null)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey(true);
                return;
            }

            contactFromInput = GetContactFromConsole(contactFromInput.Id);
            contactRepository.UpdateContact(contactFromInput);

            Console.WriteLine();
            Console.WriteLine("Successfuly updated contact");
            Console.ReadKey(true);
        }

        private Contact GetContactFromConsole(uint contactId)
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

            return new Contact(_creatorId, firstName, lastName, email, contactId);
        }
    }
}

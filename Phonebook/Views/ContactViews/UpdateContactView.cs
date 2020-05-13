using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.ContactViews
{
    public class UpdateContactView
    {
        private uint creatorId;
        public UpdateContactView(uint _creatorId)
        {
            creatorId = _creatorId;
        }
        public void Show()
        {
            Console.Clear();
            Console.Write("Input contact's id to update:");
            bool isContactIdNumber = uint.TryParse(Console.ReadLine(), out uint contactId);

            if (!isContactIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var contactFromInput = new Contact(creatorId,contactId);

            var contactRepository = new ContactRepository();
            contactFromInput = contactRepository.ReadContact(contactFromInput);

            if (contactFromInput == null)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey(true);
                return;
            }
            
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Invalid first name.");
                return;
            }

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid last name.");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Invalid email.");
                return;
            }

            contactFromInput.FirstName = firstName;
            contactFromInput.LastName= lastName;
            contactFromInput.Email= email;

            contactRepository.UpdateContact(contactFromInput);

            Console.WriteLine();
            Console.WriteLine("Successfuly updated contact");
            Console.ReadKey();
        }
    }
}

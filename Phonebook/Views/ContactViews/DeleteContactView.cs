using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.ContactViews
{
    public class DeleteContactView
    {
        private uint creatorId;
        public DeleteContactView(uint _creatorId)
        {
            creatorId = _creatorId;
        }
        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input contact's id to delete: ");
            bool isContactIdNumber = uint.TryParse(Console.ReadLine(), out uint contactId);

            if (!isContactIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var contactFromInput = new Contact(creatorId, contactId);

            var contactRepository = new ContactRepository();
            contactFromInput = contactRepository.ReadContact(contactFromInput);

            if (contactFromInput == null)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey(true);
                return;
            }

            contactRepository.DeleteContact(contactFromInput);

            Console.WriteLine();
            Console.WriteLine("Successfuly deleted contact");
            Console.ReadKey(true);
        }
    }
}

using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class DeleteContactView : BaseContactView
    {
        public DeleteContactView(IContactRepository contactRepository) : base(contactRepository)
        { }

        public void Show(uint creatorId)
        {
            Console.WriteLine();
            Console.Write("Input contact's id to delete: ");

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

            contactRepository.DeleteContact(contactFromInput);

            Console.WriteLine();
            Console.WriteLine("Successfuly deleted contact");
            Console.ReadKey(true);
        }
    }
}

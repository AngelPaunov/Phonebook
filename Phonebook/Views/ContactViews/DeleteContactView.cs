using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class DeleteContactView : BaseContactView
    {
        public DeleteContactView(uint creatorId, IContactRepository contactRepository) : base(contactRepository, creatorId)
        { }

        public void Show()
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

            var contactFromInput = GetContactById(contactInputId);
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

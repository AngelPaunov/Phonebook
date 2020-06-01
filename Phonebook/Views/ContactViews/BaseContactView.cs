using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class BaseContactView
    {
        protected IContactRepository contactRepository;

        public BaseContactView(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        protected uint GetIdFromInput()
        {
            bool isContactIdNumber = uint.TryParse(Console.ReadLine(), out uint contactId);

            Console.WriteLine();

            if (!isContactIdNumber)
            {
                return 0;
            }

            return contactId;
        }

        protected Contact GetContactById(uint creatorId, uint contactId)
        {
            var contactFromInput = new Contact(creatorId, contactId);

            contactFromInput = contactRepository.ReadContact(contactFromInput);

            return contactFromInput;
        }
    }
}

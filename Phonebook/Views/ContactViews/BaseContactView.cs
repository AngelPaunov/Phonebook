using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class BaseContactView
    {
        protected IContactRepository contactRepository;
        protected uint creatorId;

        public BaseContactView(IContactRepository contactRepository, uint creatorId)
        {
            this.contactRepository = contactRepository;
            this.creatorId = creatorId;
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

        protected Contact GetContactById(uint contactId)
        {
            var contactFromInput = new Contact(creatorId, contactId);

            contactFromInput = contactRepository.ReadContact(contactFromInput);

            return contactFromInput;
        }
    }
}

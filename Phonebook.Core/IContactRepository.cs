using System.Collections.Generic;

namespace Phonebook.Entities
{
    public interface IContactRepository
    {
        void CreateContact(Contact newContact);

        Contact ReadContact(Contact contactToRead);

        IEnumerable<Contact> ReadAllContacts();

        void UpdateContact(Contact contactToUpdate);

        void DeleteContact(Contact contactToDelete);
    }
}

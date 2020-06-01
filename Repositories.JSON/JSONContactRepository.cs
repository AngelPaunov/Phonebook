using Newtonsoft.Json;
using Phonebook.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.Repositories.JSON
{
    public class JSONContactRepository : IContactRepository
    {
        private readonly string filePath = "contacts.json";

        public JSONContactRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void CreateContact(Contact newContact)
        {
            var jsonString = File.ReadAllText(filePath);

            var contactsList = JsonConvert.DeserializeObject<List<Contact>>(jsonString) ?? new List<Contact>();
            var lastContact = contactsList.LastOrDefault();

            newContact.Id = lastContact == null ? 1 : lastContact.Id + 1;
            contactsList.Add(newContact);

            var jsonFinalFile = JsonConvert.SerializeObject(contactsList, Formatting.Indented);
            File.WriteAllText(filePath, jsonFinalFile);
        }

        public void DeleteContact(Contact contactToDelete)
        {
            string jsonString = File.ReadAllText(filePath);
            var contactList = JsonConvert.DeserializeObject<Contact[]>(jsonString).Where(c => c.Id != contactToDelete.Id);
            var jsonFinalFile = JsonConvert.SerializeObject(contactList, Formatting.Indented);

            File.WriteAllText(filePath, jsonFinalFile);
        }

        public IEnumerable<Contact> ReadAllContacts()
        {
            var jsonString = File.ReadAllText(filePath);
            var contactsList = JsonConvert.DeserializeObject<Contact[]>(jsonString);

            return contactsList;
        }

        public Contact ReadContact(Contact contactToRead)
        {
            var jsonString = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<Contact[]>(jsonString)?.FirstOrDefault(c => c.Id == contactToRead.Id);
        }

        public void UpdateContact(Contact contactToUpdate)
        {
            string jsonString = File.ReadAllText(filePath);
            var contactList = JsonConvert.DeserializeObject<Contact[]>(jsonString);
            var contactIndex = contactToUpdate.Id - 1;
            contactList[contactIndex] = new Contact(contactToUpdate);

            var jsonFinalFile = JsonConvert.SerializeObject(contactList, Formatting.Indented);
            File.WriteAllText(filePath, jsonFinalFile);
        }
    }
}

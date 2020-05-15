using Phonebook.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.CSVRepositories
{
    public class CSVContactRepository : IContactRepository
    {
        private readonly string filePath = "Contacts.txt";

        public CSVContactRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void CreateContact(Contact newContact)
        {
            Contact lastContact = ReadAllContacts().LastOrDefault(c => c.CreatorId == newContact.CreatorId);

            if (lastContact == null)
            {
                newContact.Id = 1;
            }
            else
            {
                newContact.Id = lastContact.Id + 1;
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{newContact.CreatorId},{newContact.Id},{newContact.FirstName},{newContact.LastName},{newContact.Email}");
            }
        }

        public Contact ReadContact(Contact contactToRead)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var contact = GetContactFromCSVLine(reader.ReadLine());

                    if (contact.Id == contactToRead.Id && contact.CreatorId == contactToRead.CreatorId)
                    {
                        return contact;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Contact> ReadAllContacts()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var contact = GetContactFromCSVLine(reader.ReadLine());

                    yield return contact;
                }
            }
        }

        public void UpdateContact(Contact contactToUpdate)
        {
            string temporaryFilePath = "temporaryFile.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (StreamWriter writer = new StreamWriter(temporaryFilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var contact = GetContactFromCSVLine(reader.ReadLine());

                        if (contact.Id == contactToUpdate.Id && contact.CreatorId == contactToUpdate.CreatorId)
                        {
                            writer.WriteLine($"{contactToUpdate.CreatorId},{contactToUpdate.Id},{contactToUpdate.FirstName},{contactToUpdate.LastName},{contactToUpdate.Email}");
                            continue;
                        }

                        writer.WriteLine($"{contact.CreatorId},{contact.Id},{contact.FirstName},{contact.LastName},{contact.Email}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        public void DeleteContact(Contact contactToDelete)
        {
            string temporaryFilePath = "temporaryFile.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (StreamWriter writer = new StreamWriter(temporaryFilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var contact = GetContactFromCSVLine(reader.ReadLine());

                        if (contact.CreatorId == contactToDelete.CreatorId && contact.Id == contactToDelete.Id)
                        {
                            continue;
                        }

                        if (contact.CreatorId == contactToDelete.CreatorId && contact.Id > contactToDelete.Id)
                        {
                            writer.WriteLine($"{contact.CreatorId},{contact.Id - 1},{contact.FirstName},{contact.LastName},{contact.Email}");
                            continue;
                        }

                        writer.WriteLine($"{contact.CreatorId},{contact.Id},{contact.FirstName},{contact.LastName},{contact.Email}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        private Contact GetContactFromCSVLine(string line)
        {
            string[] contactData = line.Split(',');

            uint creatorId = uint.Parse(contactData[0]);
            uint contactId = uint.Parse(contactData[1]);
            string firstName = contactData[2];
            string lastName = contactData[3];
            string email = contactData[4];

            return new Contact(creatorId, firstName, lastName, email, contactId);
        }
    }
}

using Phonebook.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Phonebook.Repositories.XML
{
    public class XMLContactRepository : IContactRepository
    {
        private readonly string filePath = "contacts.xml";
        private readonly XDocument xmlFile;

        public XMLContactRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                xmlFile = new XDocument();
                xmlFile.Add(new XElement("Contacts"));
                xmlFile.Save(filePath);
            }

            xmlFile = XDocument.Load(filePath);
        }

        public void CreateContact(Contact newContact)
        {
            var lastContact = ReadAllContacts().LastOrDefault(c => c.CreatorId == newContact.CreatorId);

            newContact.Id = lastContact == null ? 1 : lastContact.Id + 1;

            var currentTimeAsUtc = DateTime.UtcNow;
            XElement newXmlContact = new XElement("Contact",
                            new XElement("creatorId", newContact.CreatorId),
                            new XElement("id", newContact.Id),
                            new XElement("firstName", newContact.FirstName),
                            new XElement("lastName", newContact.LastName),
                            new XElement("email", newContact.Email),
                            new XElement("createDate", currentTimeAsUtc),
                            new XElement("updateDate", currentTimeAsUtc));
            xmlFile.Root.Add(newXmlContact);
            xmlFile.Save(filePath);
        }

        public Contact ReadContact(Contact ContactToRead)
        {
            var contactAsXmlElement = xmlFile.Descendants("Contact").FirstOrDefault(c => c.Element("id").Value == ContactToRead.Id.ToString() && c.Element("creatorId").Value == ContactToRead.CreatorId.ToString());

            if (contactAsXmlElement is null) return null;

            var ContactFromXml = new Contact()
            {
                CreatorId = uint.Parse(contactAsXmlElement.Element("creatorId").Value),
                Id = uint.Parse(contactAsXmlElement.Element("id").Value),
                FirstName = contactAsXmlElement.Element("firstName").Value,
                LastName = contactAsXmlElement.Element("lastName").Value,
                Email = contactAsXmlElement.Element("email").Value,
                CreateDate = DateTime.Parse(contactAsXmlElement.Element("createDate").Value),
                UpdateDate = DateTime.Parse(contactAsXmlElement.Element("updateDate").Value)
            };

            return ContactFromXml;
        }

        public IEnumerable<Contact> ReadAllContacts()
        {
            var ContactsFromXml = xmlFile.Descendants("Contact").Select(c => new Contact()
            {
                CreatorId = uint.Parse(c.Element("creatorId").Value),
                Id = uint.Parse(c.Element("id").Value),
                FirstName = c.Element("firstName").Value,
                LastName = c.Element("lastName").Value,
                Email = c.Element("email").Value,
                CreateDate = DateTime.Parse(c.Element("createDate").Value),
                UpdateDate = DateTime.Parse(c.Element("updateDate").Value)
            });
            return ContactsFromXml;
        }

        public void UpdateContact(Contact ContactToUpdate)
        {
            var ContactAsXmlElement = xmlFile.Descendants("Contact").FirstOrDefault(c => c.Element("id").Value == ContactToUpdate.Id.ToString() && c.Element("creatorId").Value == ContactToUpdate.CreatorId.ToString());
            ContactAsXmlElement.Element("firstName").SetValue(ContactToUpdate.FirstName);
            ContactAsXmlElement.Element("lastName").SetValue(ContactToUpdate.LastName);
            ContactAsXmlElement.Element("email").SetValue(ContactToUpdate.Email);
            ContactAsXmlElement.Element("updateDate").SetValue(DateTime.UtcNow);
            xmlFile.Save(filePath);
        }

        public void DeleteContact(Contact ContactToDelete)
        {
            var ContactAsXmlElement = xmlFile.Descendants("Contact").FirstOrDefault(c => c.Element("id").Value == ContactToDelete.Id.ToString() && c.Element("creatorId").Value == ContactToDelete.CreatorId.ToString());
            ContactAsXmlElement.Remove();

            ReorganizeContacts(ContactToDelete);

            xmlFile.Save(filePath);
        }

        private void ReorganizeContacts(Contact ContactToDelete)
        {
            var contacts = xmlFile.Descendants("Contact").Where(c => c.Element("creatorId").Value == ContactToDelete.CreatorId.ToString());
            foreach (var contact in contacts)
            {
                contact.Element("id").SetValue((uint.Parse(contact.Element("id").Value) - 1).ToString());
            }
        }
    }
}

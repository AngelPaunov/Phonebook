using Phonebook.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Phonebook.Repositories.XML
{
    public class XMLPhoneRepository : IPhoneRepository
    {
        private readonly string filePath = "phones.xml";
        private readonly XDocument xmlFile;

        public XMLPhoneRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                xmlFile = new XDocument();
                xmlFile.Add(new XElement("Phones"));
                xmlFile.Save(filePath);
            }

            xmlFile = XDocument.Load(filePath);
        }

        public void CreatePhone(Phone newPhone)
        {
            var lastPhone = ReadAllPhones().LastOrDefault(p => p.UserId == newPhone.UserId && p.ContactId == newPhone.ContactId);

            newPhone.Id = lastPhone == null ? 1 : lastPhone.Id + 1;
            var currentTimeAsUtc = DateTime.UtcNow;
            XElement newXmlPhone = new XElement("Phone",
                            new XElement("userId", newPhone.UserId),
                            new XElement("contactId", newPhone.ContactId),
                            new XElement("id", newPhone.Id),
                            new XElement("phoneNumber", newPhone.PhoneNumber),
                            new XElement("createDate", currentTimeAsUtc),
                            new XElement("updateDate", currentTimeAsUtc));
            xmlFile.Root.Add(newXmlPhone);
            xmlFile.Save(filePath);
        }

        public Phone ReadPhone(Phone PhoneToRead)
        {
            var PhoneAsXmlElement = xmlFile.Descendants("Phone").FirstOrDefault(p => p.Element("id").Value == PhoneToRead.Id.ToString()
                                                                                && p.Element("contactId").Value == PhoneToRead.ContactId.ToString()
                                                                                && p.Element("userId").Value == PhoneToRead.UserId.ToString());

            if (PhoneAsXmlElement is null) return null;

            var PhoneFromXml = new Phone()
            {
                UserId = uint.Parse(PhoneAsXmlElement.Element("userId").Value),
                ContactId = uint.Parse(PhoneAsXmlElement.Element("contactId").Value),
                Id = uint.Parse(PhoneAsXmlElement.Element("id").Value),
                PhoneNumber = PhoneAsXmlElement.Element("phoneNumber").Value,
                CreateDate = DateTime.Parse(PhoneAsXmlElement.Element("createDate").Value),
                UpdateDate = DateTime.Parse(PhoneAsXmlElement.Element("updateDate").Value)
            };

            return PhoneFromXml;
        }

        public IEnumerable<Phone> ReadAllPhones()
        {
            var PhonesFromXml = xmlFile.Descendants("Phone").Select(p => new Phone()
            {
                UserId = uint.Parse(p.Element("userId").Value),
                ContactId = uint.Parse(p.Element("contactId").Value),
                Id = uint.Parse(p.Element("id").Value),
                PhoneNumber = p.Element("phoneNumber").Value,
                CreateDate = DateTime.Parse(p.Element("createDate").Value),
                UpdateDate = DateTime.Parse(p.Element("updateDate").Value)
            });
            return PhonesFromXml;
        }

        public void UpdatePhone(Phone PhoneToUpdate)
        {
            var PhoneAsXmlElement = xmlFile.Descendants("Phone").FirstOrDefault(p => p.Element("id").Value == PhoneToUpdate.Id.ToString()
                                                                                && p.Element("userId").Value == PhoneToUpdate.UserId.ToString()
                                                                                && p.Element("contactId").Value == PhoneToUpdate.ContactId.ToString());
            PhoneAsXmlElement.Element("phoneNumber").SetValue(PhoneToUpdate.PhoneNumber);
            PhoneAsXmlElement.Element("updateDate").SetValue(DateTime.UtcNow);
            xmlFile.Save(filePath);
        }

        public void DeletePhone(Phone PhoneToDelete)
        {
            var PhoneAsXmlElement = xmlFile.Descendants("Phone").FirstOrDefault(p => p.Element("id").Value == PhoneToDelete.Id.ToString()
                                                                                && p.Element("contactId").Value == PhoneToDelete.ContactId.ToString()
                                                                                && p.Element("userId").Value == PhoneToDelete.UserId.ToString());
            PhoneAsXmlElement.Remove();

            ReorganizePhones(PhoneToDelete);

            xmlFile.Save(filePath);
        }

        private void ReorganizePhones(Phone PhoneToDelete)
        {
            var phones = xmlFile.Descendants("Contact").Where(c => c.Element("userId").Value == PhoneToDelete.UserId.ToString() && c.Element("contactId").Value == PhoneToDelete.ContactId.ToString());
            foreach (var phone in phones)
            {
                phone.Element("id").SetValue((uint.Parse(phone.Element("id").Value) - 1).ToString());
            }
        }
    }
}

using Phonebook.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Phonebook.Repositories.XML
{
    public class XMLUserRepository : IUserRepository
    {
        private readonly string filePath = "users.xml";
        private readonly XDocument xmlFile;

        public XMLUserRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                xmlFile = new XDocument();
                xmlFile.Add(new XElement("Users"));
                CreateUser(new User("admin", "adminpass", "admin", "admin", true));
            }

            xmlFile = XDocument.Load(filePath);
        }

        public void CreateUser(User newUser)
        {
            var lastUser = ReadAllUsers().LastOrDefault();

            newUser.Id = lastUser == null ? 1 : lastUser.Id + 1;
            XElement newXmlUser = new XElement("User",
                            new XElement("id", newUser.Id),
                            new XElement("username", newUser.Username),
                            new XElement("password", newUser.Password),
                            new XElement("firstName", newUser.FirstName),
                            new XElement("lastName", newUser.LastName),
                            new XElement("isAdmin", newUser.IsAdmin));
            xmlFile.Root.Add(newXmlUser);
            xmlFile.Save(filePath);
        }

        public User ReadUser(User userToRead)
        {
            var userAsXmlElement = xmlFile.Descendants("User").FirstOrDefault(u => u.Element("id").Value == userToRead.Id.ToString() || u.Element("username").Value == userToRead.Username);

            if (userAsXmlElement is null) return null;

            var userFromXml = new User()
            {
                Id = uint.Parse(userAsXmlElement.Element("id").Value),
                Username = userAsXmlElement.Element("username").Value,
                Password = userAsXmlElement.Element("password").Value,
                FirstName = userAsXmlElement.Element("firstName").Value,
                LastName = userAsXmlElement.Element("lastName").Value,
                IsAdmin = bool.Parse(userAsXmlElement.Element("isAdmin").Value)
            };

            return userFromXml;
        }

        public IEnumerable<User> ReadAllUsers()
        {
            var usersFromXml = xmlFile.Descendants("User").Select(u => new User()
            {
                Id = uint.Parse(u.Element("id").Value),
                Username = u.Element("username").Value,
                Password = u.Element("password").Value,
                FirstName = u.Element("firstName").Value,
                LastName = u.Element("lastName").Value,
                IsAdmin = bool.Parse(u.Element("isAdmin").Value)
            });
            return usersFromXml;
        }

        public void UpdateUser(User userToUpdate)
        {
            var userAsXmlElement = xmlFile.Descendants("User").FirstOrDefault(u => u.Element("id").Value == userToUpdate.Id.ToString());
            userAsXmlElement.Element("username").SetValue(userToUpdate.Username);
            userAsXmlElement.Element("password").SetValue(userToUpdate.Password);
            userAsXmlElement.Element("firstName").SetValue(userToUpdate.FirstName);
            userAsXmlElement.Element("lastName").SetValue(userToUpdate.LastName);
            userAsXmlElement.Element("isAdmin").SetValue(userToUpdate.IsAdmin);
            xmlFile.Save(filePath);
        }

        public void DeleteUser(User userToDelete)
        {
            var userAsXmlElement = xmlFile.Descendants("User").FirstOrDefault(u => u.Element("id").Value == userToDelete.Id.ToString());
            userAsXmlElement.Remove();
            xmlFile.Save(filePath);
        }
    }
}

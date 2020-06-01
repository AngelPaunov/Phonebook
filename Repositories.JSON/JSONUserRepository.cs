using Newtonsoft.Json;
using Phonebook.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.Repositories.JSON
{
    public class JSONUserRepository : IUserRepository
    {
        private readonly string filePath = "users.json";

        public JSONUserRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                CreateUser(new User("admin", "adminpass", "admin", "admin", true));
            }
        }

        public void CreateUser(User newUser)
        {
            var jsonString = File.ReadAllText(filePath);

            var usersList = JsonConvert.DeserializeObject<List<User>>(jsonString) ?? new List<User>();
            var lastUser = usersList.LastOrDefault();

            newUser.Id = lastUser == null ? 1 : lastUser.Id + 1;
            usersList.Add(newUser);

            var jsonFinalFile = JsonConvert.SerializeObject(usersList, Formatting.Indented);
            File.WriteAllText(filePath, jsonFinalFile);
        }

        public void DeleteUser(User userToDelete)
        {
            string jsonString = File.ReadAllText(filePath);
            var userList = JsonConvert.DeserializeObject<User[]>(jsonString).Where(u => u.Id != userToDelete.Id);
            var jsonFinalFile = JsonConvert.SerializeObject(userList, Formatting.Indented);

            File.WriteAllText(filePath, jsonFinalFile);
        }

        public IEnumerable<User> ReadAllUsers()
        {
            string jsonString = File.ReadAllText(filePath);
            var userList = JsonConvert.DeserializeObject<User[]>(jsonString);

            return userList;
        }

        public User ReadUser(User userToRead)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<User[]>(jsonString)?.SingleOrDefault(u => u.Id == userToRead.Id || u.Username == userToRead.Username);
        }

        public void UpdateUser(User userToUpdate)
        {
            string jsonString = File.ReadAllText(filePath);
            var userList = JsonConvert.DeserializeObject<User[]>(jsonString);
            var userIndex = userToUpdate.Id - 1;
            userList[userIndex] = new User(userToUpdate);

            var jsonFinalFile = JsonConvert.SerializeObject(userList, Formatting.Indented);
            File.WriteAllText(filePath, jsonFinalFile);
        }
    }
}

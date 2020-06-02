using Phonebook.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.Repositories.CSV
{
    public class CSVUserRepository : IUserRepository
    {
        private readonly string filePath = "Users.txt";

        public CSVUserRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();

                CreateUser(new User("admin", "adminpass", "admin", "admin", true));
            }
        }

        public void CreateUser(User newUser)
        {
            User lastUser = ReadAllUsers().LastOrDefault();

            if (lastUser == null)
            {
                newUser.Id = 1;
            }
            else
            {
                newUser.Id = lastUser.Id + 1;
            }

            var currentTimeAsUtc = DateTime.UtcNow;
            newUser.CreateDate = currentTimeAsUtc;
            newUser.UpdateDate = currentTimeAsUtc;
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{newUser.Id},{newUser.Username},{newUser.Password},{newUser.FirstName},{newUser.LastName},{newUser.IsAdmin},{newUser.CreateDate},{newUser.UpdateDate}");
            }
        }

        public User ReadUser(User userToRead)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var user = GetUserFromCSVLine(reader.ReadLine());

                    if (user.Id == userToRead.Id || user.Username == userToRead.Username) return user;
                }
            }
            return null;
        }

        public IEnumerable<User> ReadAllUsers()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var user = GetUserFromCSVLine(reader.ReadLine());

                    yield return user;
                }
            }
        }

        public void UpdateUser(User userToUpdate)
        {
            string temporaryFilePath = "temporaryFile.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (StreamWriter writer = new StreamWriter(temporaryFilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var user = GetUserFromCSVLine(reader.ReadLine());

                        if (user.Id == userToUpdate.Id)
                        {
                            userToUpdate.CreateDate = user.CreateDate;
                            userToUpdate.UpdateDate = DateTime.UtcNow;
                            writer.WriteLine($"{userToUpdate.Id},{userToUpdate.Username},{userToUpdate.Password},{userToUpdate.FirstName},{userToUpdate.LastName},{userToUpdate.IsAdmin},{userToUpdate.CreateDate},{userToUpdate.UpdateDate}");
                            continue;
                        }

                        writer.WriteLine($"{user.Id},{user.Username},{user.Password},{user.FirstName},{user.LastName},{user.IsAdmin},{user.CreateDate},{user.UpdateDate}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        public void DeleteUser(User userToDelete)
        {
            string temporaryFilePath = "temporaryFile.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (StreamWriter writer = new StreamWriter(temporaryFilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var user = GetUserFromCSVLine(reader.ReadLine());

                        if (user.Id == userToDelete.Id)
                        {
                            continue;
                        }

                        if (user.Id > userToDelete.Id)
                        {
                            writer.WriteLine($"{user.Id - 1},{user.Username},{user.Password},{user.FirstName},{user.LastName},{user.IsAdmin},{user.CreateDate},{user.UpdateDate}");
                            continue;
                        }

                        writer.WriteLine($"{user.Id},{user.Username},{user.Password},{user.FirstName},{user.LastName},{user.IsAdmin},{user.CreateDate},{user.UpdateDate}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        private User GetUserFromCSVLine(string line)
        {
            string[] userData = line.Split(',');

            var userFromCSV = new User
            {
                Id = uint.Parse(userData[0]),
                Username = userData[1],
                Password = userData[2],
                FirstName = userData[3],
                LastName = userData[4],
                IsAdmin = bool.Parse(userData[5]),
                CreateDate = DateTime.Parse(userData[6]),
                UpdateDate = DateTime.Parse(userData[7])
            };
            return userFromCSV;
        }
    }
}

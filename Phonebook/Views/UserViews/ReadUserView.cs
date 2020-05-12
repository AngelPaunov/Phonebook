using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.UserViews
{
    public class ReadUserView
    {
        public void Show()
        {
            Console.WriteLine();

            Console.Write("Input user's id to update:");
            bool isUserIdNumber = uint.TryParse(Console.ReadLine(), out uint userInputId);

            Console.WriteLine();

            if (!isUserIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var userFromInput = new User(userInputId);

            UserRepository userRepository = new UserRepository();
            var userFromRepository = userRepository.ReadUser(userFromInput);

            if (userFromRepository == null)
            {
                Console.WriteLine("Invalid user id. User not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine($"ID: {userFromRepository.Id}");
            Console.WriteLine($"Username: {userFromRepository.Username}");
            Console.WriteLine($"Password: {userFromRepository.Password}");
            Console.WriteLine($"First Name: {userFromRepository.FirstName}");
            Console.WriteLine($"Last Name: {userFromRepository.LastName}");
            Console.WriteLine($"Admin: {userFromRepository.IsAdmin}");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return;
        }
    }
}

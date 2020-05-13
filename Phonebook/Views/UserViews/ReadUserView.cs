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
            userFromInput = userRepository.ReadUser(userFromInput);

            if (userFromInput == null)
            {
                Console.WriteLine("Invalid user id. User not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine($"ID: {userFromInput.Id}");
            Console.WriteLine($"Username: {userFromInput.Username}");
            Console.WriteLine($"Password: {userFromInput.Password}");
            Console.WriteLine($"First Name: {userFromInput.FirstName}");
            Console.WriteLine($"Last Name: {userFromInput.LastName}");
            Console.WriteLine($"Admin: {userFromInput.IsAdmin}");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return;
        }
    }
}

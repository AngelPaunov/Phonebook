using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.UserViews
{
    public class DeleteUserView
    {
        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input user's id to delete: ");
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

            userRepository.DeleteUser(userFromRepository);
            Console.WriteLine("User has been deleted.");
            Console.ReadKey(true);
        }
    }
}

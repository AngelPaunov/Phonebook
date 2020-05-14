using Phonebook.Entities;
using Phonebook.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Views.UserViews
{
    public class BaseUserView
    {
        public uint GetIdFromInput()
        {
            bool isUserIdNumber = uint.TryParse(Console.ReadLine(), out uint userInputId);

            Console.WriteLine();

            if (!isUserIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return 0;
            }

            return userInputId;
        }

        public User GetUserById(uint userId) {
            var userFromInput = new User(userId);

            UserRepository userRepository = new UserRepository();
            userFromInput = userRepository.ReadUser(userFromInput);

            if (userFromInput == null)
            {
                Console.WriteLine("Invalid user id. User not found.");
                Console.ReadKey(true);
            }
            return userFromInput;
        }
    }
}

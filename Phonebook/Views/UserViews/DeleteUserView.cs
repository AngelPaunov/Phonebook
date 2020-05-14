using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.UserViews
{
    public class DeleteUserView : BaseUserView
    {
        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input user's id to delete: ");
            //bool isUserIdNumber = uint.TryParse(Console.ReadLine(), out uint userInputId);

            //Console.WriteLine();

            //if (!isUserIdNumber)
            //{
            //    Console.WriteLine("Please input positive number.");
            //    Console.ReadKey();
            //    return;
            //}
            uint userInput = GetIdFromInput();
            if (userInput < 1)
            {
                return;
            }

            //var userFromInput = new User(userInputId);

            //UserRepository userRepository = new UserRepository();
            //userFromInput = userRepository.ReadUser(userFromInput);

            //if (userFromInput == null)
            //{
            //    Console.WriteLine("Invalid user id. User not found.");
            //    Console.ReadKey(true);
            //    return;
            //}
            var userFromInput = GetUserById(userInput);
            if (userFromInput == null)
            {
                return;
            }

            var userRepository = new UserRepository();
            userRepository.DeleteUser(userFromInput);
            Console.WriteLine("User has been deleted.");
            Console.ReadKey(true);
        }
    }
}

using Phonebook.Entities;
using Phonebook.Repositories.CSV;
using System;

namespace Phonebook.Views.UserViews
{
    public class DeleteUserView : BaseUserView
    {
        public DeleteUserView(IUserRepository userRepository) : base(userRepository)
        { }

        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input user's id to delete: ");

            uint userInputId = GetIdFromInput();
            if (userInputId < 1)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var userFromInput = GetUserById(userInputId);
            if (userFromInput == null)
            {
                Console.WriteLine("Invalid user id. User not found.");
                Console.ReadKey(true);
                return;
            }

            _userRepository.DeleteUser(userFromInput);

            Console.WriteLine("User has been deleted.");
            Console.ReadKey(true);
        }
    }
}

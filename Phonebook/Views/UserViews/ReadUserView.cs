using Phonebook.Entities;
using System;

namespace Phonebook.Views.UserViews
{
    public class ReadUserView : BaseUserView
    {
        public ReadUserView(IUserRepository userRepository) : base(userRepository)
        { }

        public void Show()
        {
            Console.WriteLine();

            Console.Write("Input user's id to check: ");

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

            Console.WriteLine($"ID: {userFromInput.Id}");
            Console.WriteLine($"Username: {userFromInput.Username}");
            Console.WriteLine($"First Name: {userFromInput.FirstName}");
            Console.WriteLine($"Last Name: {userFromInput.LastName}");
            Console.WriteLine($"Admin: {userFromInput.IsAdmin}");
            Console.WriteLine($"Create Date: {userFromInput.CreateDate.ToLocalTime()}");
            Console.WriteLine($"Update Date: {userFromInput.UpdateDate.ToLocalTime()}");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return;
        }
    }
}

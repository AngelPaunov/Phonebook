using Phonebook.Entities;
using System;

namespace Phonebook.Views.UserViews
{
    public class ReadAllUsersView : BaseUserView
    {
        public ReadAllUsersView(IUserRepository userRepository) : base(userRepository)
        { }

        public void Show()
        {
            Console.Clear();

            foreach (var user in _userRepository.ReadAllUsers())
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Admin: {user.IsAdmin}");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ReadKey();
            return;
        }
    }
}

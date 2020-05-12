using Phonebook.Repositories;
using System;

namespace Phonebook.Views.UserViews
{
    public class ReadAllUsersView
    {
        public void Show()
        {
            Console.Clear();

            var userRepository = new UserRepository();

            //TODO: get all users from db

            foreach (var user in userRepository.ReadAllUsers())
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Password: {user.Password}");
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

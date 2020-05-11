using System;

namespace Phonebook.Views.User
{
    public class ReadUserView
    {
        public void Show()
        {
            Console.WriteLine();

            //TODO: get user from db

            //Console.WriteLine($"ID: {user.Id}");
            //Console.WriteLine($"Username: {user.Username}");
            //Console.WriteLine($"Password: {user.Password}");
            //Console.WriteLine($"First Name: {user.FirstName}");
            //Console.WriteLine($"Last Name: {user.LastName}");
            //Console.WriteLine($"Admin: {user.IsAdmin}");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return;
        }
    }
}

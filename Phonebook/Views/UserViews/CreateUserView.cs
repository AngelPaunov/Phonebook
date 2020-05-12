using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.UserViews
{
    public class CreateUserView
    {
        public void Show()
        {
            Console.Clear();
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Invalid username.");
                Console.ReadKey(true);
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.Write("Invalid password.");
                Console.ReadKey(true);
                return;
            }

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Invalid first name.");
                Console.ReadKey(true);
                return;
            }

            Console.Write("Last Name: ");
            string lastName= Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid last name.");
                Console.ReadKey(true);
                return;
            }

            Console.Write("Admin (true/false): ");
            bool isAdminValueBool= bool.TryParse(Console.ReadLine(), out bool isAdmin);

            if (!isAdminValueBool)
            {
                Console.WriteLine("Invalid admin value.");
                Console.ReadKey(true);
                return;
            }

            UserRepository userRepository = new UserRepository();
            userRepository.CreateUser(new User(username,password,firstName,lastName,isAdmin));
        }
    }
}

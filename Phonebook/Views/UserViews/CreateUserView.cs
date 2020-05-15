using Phonebook.Entities;
using Phonebook.CSVRepositories;
using System;

namespace Phonebook.Views.UserViews
{
    public class CreateUserView :BaseUserView
    {
        public CreateUserView(IUserRepository userRepository):base (userRepository)
        { }
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
            string password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            Console.WriteLine();

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

            userRepository.CreateUser(new User(username,password,firstName,lastName,isAdmin));

            Console.WriteLine("Successfuly created user.");
            Console.ReadKey(true);
        }
    }
}

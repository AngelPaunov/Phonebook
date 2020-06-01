using Phonebook.Entities;
using System;

namespace Phonebook.Views.UserViews
{
    public class UpdateUserView : BaseUserView
    {
        public UpdateUserView(IUserRepository userRepository) : base(userRepository)
        { }

        public void Show()
        {
            Console.Clear();
            Console.Write("Input user's id to update: ");

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

            userFromInput = GetUserFromConsole();
            userFromInput.Id = userInputId;
            _userRepository.UpdateUser(userFromInput);

            Console.WriteLine("User has been updated.");
            Console.ReadKey(true);
        }

        private User GetUserFromConsole()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Invalid username.");
                Console.ReadKey(true);
                return null;
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
                return null;
            }

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid last name.");
                Console.ReadKey(true);
                return null;
            }

            Console.Write("Admin (true/false): ");
            bool isAdminValueBool = bool.TryParse(Console.ReadLine(), out bool isAdmin);

            if (!isAdminValueBool)
            {
                Console.WriteLine("Invalid admin value.");
                Console.ReadKey(true);
                return null;
            }

            return new User(username, password, firstName, lastName, isAdmin);
        }
    }
}

using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views
{
    public class LoginView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Input your credentials");
                Console.Write($"Username: ");
                string username = Console.ReadLine();


                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Please input valid username.");
                    Console.ReadKey(true);
                    continue;
                }

                string password = "";
                Console.Write($"Password: ");
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

                var loginUser = new User(username, password);

                UserRepository userRepository = new UserRepository();
                var userFromRepository = userRepository.ReadUser(loginUser);

                if (userFromRepository == null || userFromRepository.Password != loginUser.Password)
                {
                    Console.WriteLine("Invalid username or password. User not found.");
                    Console.ReadKey(true);
                    continue;
                }

                var isAdmin = userFromRepository.IsAdmin;
                if (isAdmin)
                {
                    var adminView = new AdminView(userFromRepository.Id);
                    adminView.Show();
                }
                else
                {
                    var userView = new UserView(userFromRepository.Id);
                    userView.Show();
                }
            }
        }
    }
}

using Phonebook.Entities;
using System;

namespace Phonebook.Views
{
    public class LoginView
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public LoginView(IUserRepository userRepository, IServiceProvider serviceProvider)
        {
            this._userRepository = userRepository;
            this._serviceProvider = serviceProvider;
        }
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                var loginUser = GetUserFromConsole();
                var userFromRepository = this._userRepository.ReadUser(loginUser);

                if (userFromRepository == null || userFromRepository.Password != loginUser.Password)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid username or password. User not found.");
                    Console.ReadKey(true);
                    continue;
                }

                var isAdmin = userFromRepository.IsAdmin;
                if (isAdmin)
                {
                    var adminView = (AdminView)this._serviceProvider.GetService(typeof(AdminView));
                    adminView.Show(userFromRepository.Id);
                }
                else
                {
                    var userView = (UserView)this._serviceProvider.GetService(typeof(UserView));
                    userView.Show(userFromRepository.Id);
                }
            }
        }

        private User GetUserFromConsole()
        {
            Console.WriteLine($"Input your credentials");
            Console.Write($"Username: ");
            string username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Please input valid username.");
                Console.ReadKey(true);
                return null;
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

            return new User(username, password);
        }
    }
}

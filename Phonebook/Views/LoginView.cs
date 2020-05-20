using Microsoft.Extensions.DependencyInjection;
using Phonebook.CSVRepositories;
using Phonebook.Entities;
using System;

namespace Phonebook.Views
{
    public class LoginView
    {
        private IServiceProvider serviceProvider;

        public LoginView()
        {
            ConfigureRepositories();
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                var loginUser = GetUserFromConsole();
                var userRepository = serviceProvider.GetService<IUserRepository>();
                var userFromRepository = userRepository.ReadUser(loginUser);

                if (userFromRepository == null || userFromRepository.Password != loginUser.Password)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid username or password. User not found.");
                    Console.ReadKey(true);
                    continue;
                }

                var contactRepository = serviceProvider.GetService<IContactRepository>();
                var phoneRepository = serviceProvider.GetService<IPhoneRepository>();
                var isAdmin = userFromRepository.IsAdmin;
                if (isAdmin)
                {
                    var adminView = new AdminView(userFromRepository.Id, userRepository, contactRepository, phoneRepository);
                    adminView.Show();
                }
                else
                {
                    var userView = new UserView(userFromRepository.Id, contactRepository, phoneRepository);
                    userView.Show();
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

        private void ConfigureRepositories()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped<IUserRepository, CSVUserRepository>()
                .AddScoped<IContactRepository, CSVContactRepository>()
                .AddScoped<IPhoneRepository, CSVPhoneRepository>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Phonebook.CSVRepositories;
using Phonebook.Entities;
using System;

namespace Phonebook.Views
{
    class HomeView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to our app");

                RenderMenu();

                var userChoice = GetChoice();

                if (HandleChoice(userChoice))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Returns true when Show should return
        /// </summary>
        private bool HandleChoice(MenuEnum userChoice)
        {
            switch (userChoice)
            {
                case MenuEnum.Login:
                    var loginView = ConfigureLoginView();
                    loginView.Show();
                    return false;
                case MenuEnum.Exit:
                    return true;
                case MenuEnum.Invalid:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey(true);
                    return false;
            }

            return false;
        }

        private MenuEnum GetChoice()
        {
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.L:
                    return MenuEnum.Login;
                case ConsoleKey.E:
                    return MenuEnum.Exit;
                default:
                    return MenuEnum.Invalid;
            }
        }

        private void RenderMenu()
        {
            Console.WriteLine("[L]ogin");
            Console.WriteLine("[E]xit");
        }

        private LoginView ConfigureLoginView()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IUserRepository, CSVUserRepository>()
                .AddSingleton<IContactRepository, CSVContactRepository>()
                .AddSingleton<IPhoneRepository, CSVPhoneRepository>()
                .AddTransient<LoginView>()
                .BuildServiceProvider();

            return serviceProvider.GetService<LoginView>();
        }

        private enum MenuEnum
        {
            Login,
            Exit,
            Invalid
        }
    }
}

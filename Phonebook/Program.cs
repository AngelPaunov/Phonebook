using Microsoft.Extensions.DependencyInjection;
using Phonebook.CSVRepositories;
using Phonebook.Entities;
using Phonebook.Views;
using System;

namespace Phonebook
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureRepositories();
            serviceProvider.GetService<HomeView>().Show();

            Console.Clear();
            Console.WriteLine("Thanks for using Phonebook. Have a nice day!");
            Console.ReadKey(true);
        }

        private static void ConfigureRepositories()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped<IUserRepository, CSVUserRepository>()
                .AddScoped<IContactRepository, CSVContactRepository>()
                .AddScoped<IPhoneRepository, CSVPhoneRepository>()
                .AddTransient<HomeView>()
                .AddTransient<LoginView>()
                .AddTransient<UserView>()
                .AddTransient<AdminView>();

            // TODO: ...Register each view

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

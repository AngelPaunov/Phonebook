using Microsoft.Extensions.DependencyInjection;
using Phonebook.Entities;
using Phonebook.Repositories.CSV;
using Phonebook.Repositories.JSON;
using Phonebook.Repositories.XML;
using Phonebook.Views;
using Phonebook.Views.ContactViews;
using Phonebook.Views.PhoneViews;
using Phonebook.Views.UserViews;
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
                .AddTransient<AdminView>()
                .AddTransient<UserModifyView>()
                .AddTransient<ContactModifyView>()
                .AddTransient<PhoneModifyView>()

                .AddTransient<CreateUserView>()
                .AddTransient<ReadUserView>()
                .AddTransient<ReadAllUsersView>()
                .AddTransient<DeleteUserView>()
                .AddTransient<UpdateUserView>()

                .AddTransient<CreateContactView>()
                .AddTransient<ReadContactView>()
                .AddTransient<ReadAllContactsView>()
                .AddTransient<UpdateContactView>()
                .AddTransient<DeleteContactView>()

                .AddTransient<CreatePhoneView>()
                .AddTransient<ReadPhoneView>()
                .AddTransient<ReadAllPhonesView>()
                .AddTransient<UpdatePhoneView>()
                .AddTransient<DeletePhoneView>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

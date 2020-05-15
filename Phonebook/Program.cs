using Autofac;
using Phonebook.CSVRepositories;
using Phonebook.Entities;
using Phonebook.Views;
using System;

namespace Phonebook
{
    class Program
    {
        private static IContainer Container;

        static void Main(string[] args)
        {
            //var userRepository = new CSVUserRepository();
            //var contactRepository = new CSVContactRepository();
            //var phoneRepository = new CSVPhoneRepository();

            //var homeMenu = new HomeView(userRepository, contactRepository, phoneRepository);

            ConfigureContainer();
            var homeMenu = ComposeObjects();
            homeMenu.Show();

            Console.Clear();
            Console.WriteLine("Thanks for using Phonebook. Have a nice day!");
            Console.ReadKey(true);
        }

        private static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CSVUserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<CSVContactRepository>().As<IContactRepository>().SingleInstance();
            builder.RegisterType<CSVPhoneRepository>().As<IPhoneRepository>().SingleInstance();

            builder.RegisterType<HomeView>().SingleInstance();

            Container = builder.Build();
        }

        private static HomeView ComposeObjects()
        {
            return Container.Resolve<HomeView>();
        }
    }
}

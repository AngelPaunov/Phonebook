using Phonebook.CSVRepositories;
using Phonebook.Views;
using System;

namespace Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new CSVUserRepository();
            var contactRepository = new CSVContactRepository();
            var phoneRepository = new CSVPhoneRepository();

            var homeMenu = new HomeView(userRepository, contactRepository, phoneRepository);
            homeMenu.Show();

            Console.Clear();
            Console.WriteLine("Thanks for using Phonebook. Have a nice day!");
            Console.ReadKey(true);
        }
    }
}

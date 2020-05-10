using Phonebook.Entities;
using Phonebook.Data;
using System;
using Phonebook.Views;

namespace Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool ProgramIsRunning = true;
            //var userRepository = new BaseRepository<User>();
            //var contactRepository = new BaseRepository<Contact>();

            //while (ProgramIsRunning)
            //{
            //    ConsoleKey step = UserInterface.HomeViewModel();

            //    if (step == ConsoleKey.E)
            //    {
            //        ProgramIsRunning = false;
            //        Environment.Exit(0);
            //        continue;
            //    }

            //    while (step == ConsoleKey.L)
            //    {
            //        User loginUser = UserInterface.LoginViewModel(userRepository);

            //        if (loginUser != null)
            //        {
            //            if (loginUser.IsAdmin.HasValue)
            //                UserInterface.AdminMenu(userRepository, contactRepository);
            //            else
            //                UserInterface.UserMenu(contactRepository);
            //        }
            //        else
            //        {
            //            Console.WriteLine(Environment.NewLine + "Press ESC to return or press any key to continue");
            //            step = Console.ReadKey(true).Key != ConsoleKey.Escape ? ConsoleKey.L : ConsoleKey.Escape;
            //        }
            //    }
            //}

            var homeMenu = new HomeView();
            homeMenu.Show();

            Console.Clear();
            Console.WriteLine("Thanks for using Phonebook. Have a nice day!");
            Console.ReadKey(true);
        }
    }
}

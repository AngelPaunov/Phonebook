using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.CSVRepositories;
using Phonebook.Entities;
using Phonebook.Views;
using System;

namespace Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var homeMenu = new HomeView();
            homeMenu.Show();

            Console.Clear();
            Console.WriteLine("Thanks for using Phonebook. Have a nice day!");
            Console.ReadKey(true);
        }
    }
}

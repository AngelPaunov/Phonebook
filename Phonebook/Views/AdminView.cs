using Phonebook.Views.ContactViews;
using Phonebook.Views.UserViews;
using System;

namespace Phonebook.Views
{
    public class AdminView
    {
        private readonly IServiceProvider _serviceProvider;

        private uint _userId;

        public AdminView(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void Show(uint userId)
        {
            this._userId = userId;

            while (true)
            {
                Console.Clear();

                RenderMenu();

                var userChoice = GetChoice();

                if(HandleChoice(userChoice))
                {
                    return;
                }
            }
        }

        private bool HandleChoice(AdminMenuEnum userChoice)
        {
            switch (userChoice)
            {
                case AdminMenuEnum.Users:

                    var userModifyView = (UserModifyView)this._serviceProvider.GetService(typeof(UserModifyView));
                    userModifyView.Show();

                    return false;

                case AdminMenuEnum.Contacts:

                    var contactModifyView = (ContactModifyView)this._serviceProvider.GetService(typeof(ContactModifyView));
                    contactModifyView.Show(this._userId);

                    return false;

                case AdminMenuEnum.Exit:

                    return true;

                case AdminMenuEnum.Invalid:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey();
                    return false;
            }

            return false;
        }

        private AdminMenuEnum GetChoice()
        {
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.U:
                    return AdminMenuEnum.Users;
                case ConsoleKey.C:
                    return AdminMenuEnum.Contacts;
                case ConsoleKey.E:
                    return AdminMenuEnum.Exit;
                default:
                    return AdminMenuEnum.Invalid;
            }
        }
        
        private void RenderMenu()
        {
            Console.WriteLine("Choose to operate with:");
            Console.WriteLine("[U]sers");
            Console.WriteLine("[C]ontacts");
            Console.WriteLine("[E]xit");
        }

        private enum AdminMenuEnum
        {
            Users,
            Contacts,
            Exit,
            Invalid
        }
    }
}

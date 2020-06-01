using System;

namespace Phonebook.Views.ContactViews
{
    public class ContactModifyView
    {
        private readonly IServiceProvider _serviceProvider;
        private uint _userId;

        public ContactModifyView(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void Show(uint userId)
        {
            _userId = userId;

            while (true)
            {
                Console.Clear();

                RenderMenu();

                var userChoice = GetChoice();

                if (HandleChoice(userChoice))
                {
                    return;
                }
            }
        }

        private bool HandleChoice(ModifyMenuEnum userChoice)
        {
            switch (userChoice)
            {
                case ModifyMenuEnum.Create:
                    var createContactView = (CreateContactView)_serviceProvider.GetService(typeof(CreateContactView));
                    createContactView.Show(_userId);
                    return false;
                case ModifyMenuEnum.ReadSingle:
                    var readContactView = (ReadContactView)_serviceProvider.GetService(typeof(ReadContactView));
                    readContactView.Show(_userId);
                    return false;
                case ModifyMenuEnum.ReadAll:
                    var readAllContactsView = (ReadAllContactsView)_serviceProvider.GetService(typeof(ReadAllContactsView));
                    readAllContactsView.Show(_userId);
                    return false;
                case ModifyMenuEnum.Update:
                    var updateContactView = (UpdateContactView)_serviceProvider.GetService(typeof(UpdateContactView));
                    updateContactView.Show(_userId);
                    return false;
                case ModifyMenuEnum.Delete:
                    var deleteContactView = (DeleteContactView)_serviceProvider.GetService(typeof(DeleteContactView));
                    deleteContactView.Show(_userId);
                    return false;
                case ModifyMenuEnum.Exit:
                    return true;
                case ModifyMenuEnum.Invalid:
                    Console.WriteLine("Invalid choice. Please try again");
                    Console.ReadKey();
                    return false;
            }

            return false;
        }

        private ModifyMenuEnum GetChoice()
        {
            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.C:
                    return ModifyMenuEnum.Create;
                case ConsoleKey.R:
                    return ModifyMenuEnum.ReadSingle;
                case ConsoleKey.A:
                    return ModifyMenuEnum.ReadAll;
                case ConsoleKey.U:
                    return ModifyMenuEnum.Update;
                case ConsoleKey.D:
                    return ModifyMenuEnum.Delete;
                case ConsoleKey.X:
                    return ModifyMenuEnum.Exit;
                default:
                    return ModifyMenuEnum.Invalid;
            }
        }

        private void RenderMenu()
        {
            Console.WriteLine("[C]reate contact");
            Console.WriteLine("[R]ead contact");
            Console.WriteLine("Re[a]d all contact");
            Console.WriteLine("[U]pdate contact");
            Console.WriteLine("[D]elete contact");
            Console.WriteLine("E[x]it");
        }
    }
}

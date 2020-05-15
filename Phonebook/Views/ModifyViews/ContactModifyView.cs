using Phonebook.Entities;
using System;

namespace Phonebook.Views.ContactViews
{
    public class ContactModifyView
    {
        private readonly uint userId;
        private readonly IContactRepository contactRepository;
        private readonly IPhoneRepository phoneRepository;

        public ContactModifyView(uint _userId, IContactRepository contactRepository, IPhoneRepository phoneRepository)
        {
            userId = _userId;
            this.contactRepository = contactRepository;
            this.phoneRepository = phoneRepository;
        }

        public void Show()
        {
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
                    var createContactView = new CreateContactView(userId, contactRepository);
                    createContactView.Show();
                    return false;
                case ModifyMenuEnum.ReadSingle:
                    var readContactView = new ReadContactView(userId, contactRepository, phoneRepository);
                    readContactView.Show();
                    return false;
                case ModifyMenuEnum.ReadAll:
                    var readAllContactsView = new ReadAllContactsView(userId, contactRepository);
                    readAllContactsView.Show();
                    return false;
                case ModifyMenuEnum.Update:
                    var updateContactView = new UpdateContactView(userId, contactRepository);
                    updateContactView.Show();
                    return false;
                case ModifyMenuEnum.Delete:
                    var deleteContactView = new DeleteContactView(userId, contactRepository);
                    deleteContactView.Show();
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

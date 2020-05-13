using System;

namespace Phonebook.Views.PhoneViews
{
    public class PhoneModifyView
    {
        private uint contactId;
        public PhoneModifyView(uint _contactId)
        {
            contactId = _contactId;
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
                    var createPhoneView = new CreatePhoneView(contactId);
                    createPhoneView.Show();
                    return false;
                case ModifyMenuEnum.ReadSingle:
                    var readPhoneView = new ReadPhoneView(contactId);
                    readPhoneView.Show();
                    return false;
                case ModifyMenuEnum.ReadAll:
                    var readAllPhonesView = new ReadAllPhonesView(contactId);
                    readAllPhonesView.Show();
                    return false;
                case ModifyMenuEnum.Update:
                    var updatePhoneView = new UpdatePhoneView(contactId);
                    updatePhoneView.Show();
                    return false;
                case ModifyMenuEnum.Delete:
                    var deletePhoneView = new DeletePhoneView(contactId);
                    deletePhoneView.Show();
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
            Console.WriteLine($"Phone menu for contact: {contactId}");
            Console.WriteLine("[C]reate phone");
            Console.WriteLine("[R]ead phone");
            Console.WriteLine("Re[a]d all phones");
            Console.WriteLine("[U]pdate phone");
            Console.WriteLine("[D]elete phone");
            Console.WriteLine("E[x]it");
        }
    }
}

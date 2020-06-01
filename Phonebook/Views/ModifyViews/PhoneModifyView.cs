using Phonebook.Entities;
using System;

namespace Phonebook.Views.PhoneViews
{
    public class PhoneModifyView
    {
        private readonly IPhoneRepository phoneRepository;
        private readonly IServiceProvider _serviceProvider;

        public PhoneModifyView(IServiceProvider serviceProvider, IPhoneRepository phoneRepository)
        {
            this._serviceProvider = serviceProvider;
            this.phoneRepository = phoneRepository;
        }

        private uint _userId;
        private uint _contactId;

        public void Show(uint userId, uint contactId)
        {
            this._userId = userId;
            this._contactId = contactId;

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
                    var createPhoneView = (CreatePhoneView)_serviceProvider.GetService(typeof(CreatePhoneView));
                    createPhoneView.Show(_userId, _contactId);
                    return false;
                case ModifyMenuEnum.ReadSingle:
                    var readPhoneView = (ReadPhoneView)_serviceProvider.GetService(typeof(ReadPhoneView));
                    readPhoneView.Show(_userId, _contactId);
                    return false;
                case ModifyMenuEnum.ReadAll:
                    var readAllPhonesView = (ReadAllPhonesView)_serviceProvider.GetService(typeof(ReadAllPhonesView));
                    readAllPhonesView.Show(_userId, _contactId);
                    return false;
                case ModifyMenuEnum.Update:
                    var updatePhoneView = (UpdatePhoneView)_serviceProvider.GetService(typeof(UpdatePhoneView));
                    updatePhoneView.Show(_userId, _contactId);
                    return false;
                case ModifyMenuEnum.Delete:
                    var deletePhoneView = (DeletePhoneView)_serviceProvider.GetService(typeof(DeletePhoneView));
                    deletePhoneView.Show(_userId, _contactId);
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
            Console.WriteLine($"Phone menu for contact: {_contactId}");
            Console.WriteLine("[C]reate phone");
            Console.WriteLine("[R]ead phone");
            Console.WriteLine("Re[a]d all phones");
            Console.WriteLine("[U]pdate phone");
            Console.WriteLine("[D]elete phone");
            Console.WriteLine("E[x]it");
        }
    }
}

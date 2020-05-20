using Phonebook.Entities;
using Phonebook.Views.PhoneViews;
using System;

namespace Phonebook.Views.ContactViews
{
    public class ReadContactView : BaseContactView
    {
        private readonly IPhoneRepository phoneRepository;

        public ReadContactView(uint creatorId, IContactRepository contactRepository, IPhoneRepository phoneRepository) : base(contactRepository, creatorId)
        {
            this.phoneRepository = phoneRepository;
        }

        public void Show()
        {
            Console.WriteLine();

            Console.Write("Input contact's id to check: ");

            uint contactInputId = GetIdFromInput();
            if (contactInputId < 1)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var contactFromInput = GetContactById(contactInputId);
            if (contactFromInput == null)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine($"ID: {contactFromInput.Id}");
            Console.WriteLine($"First Name: {contactFromInput.FirstName}");
            Console.WriteLine($"Last Name: {contactFromInput.LastName}");
            Console.WriteLine($"Email: {contactFromInput.Email}");
            Console.WriteLine();
            Console.WriteLine("[P]hone menu");
            Console.WriteLine("Any key to continue");

            var input = GetChoice();

            if (HandleChoice(input, contactFromInput.CreatorId, contactFromInput.Id))
            {
                return;
            }
        }

        private bool HandleChoice(ReadContactEnum userChoice, uint userId, uint contactId)
        {
            switch (userChoice)
            {
                case ReadContactEnum.PhoneMenu:
                    var phoneModifyView = new PhoneModifyView(phoneRepository, userId, contactId);
                    phoneModifyView.Show();
                    return false;
                case ReadContactEnum.Continue:
                    return true;
            }

            return false;
        }

        private ReadContactEnum GetChoice()
        {
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.P:
                    return ReadContactEnum.PhoneMenu;
                default:
                    return ReadContactEnum.Continue;
            }
        }

        private enum ReadContactEnum
        {
            PhoneMenu,
            Continue
        }
    }
}

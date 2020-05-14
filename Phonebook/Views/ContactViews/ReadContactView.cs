using Phonebook.Entities;
using Phonebook.Repositories;
using Phonebook.Views.PhoneViews;
using System;

namespace Phonebook.Views.ContactViews
{
    public class ReadContactView
    {
        private uint creatorId;
        public ReadContactView(uint _creatorId)
        {
            creatorId = _creatorId;
        }

        public void Show()
        {
            Console.WriteLine();

            Console.Write("Input contact's id which you want to check: ");
            bool isContactIdNumber = uint.TryParse(Console.ReadLine(), out uint contactId);

            if (!isContactIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var contactFromInput = new Contact(creatorId, contactId);

            var contactRepository = new ContactRepository();
            contactFromInput = contactRepository.ReadContact(contactFromInput);

            if (contactFromInput == null)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey();
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

            if (HandleChoice(input,contactFromInput.CreatorId, contactFromInput.Id))
            {
                return;
            }
        }

        private bool HandleChoice(ReadContactEnum userChoice, uint userId, uint contactId)
        {
            switch (userChoice)
            {
                case ReadContactEnum.PhoneMenu:
                    var phoneModifyView = new PhoneModifyView(userId, contactId);
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

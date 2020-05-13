using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.PhoneViews
{
    public class DeletePhoneView
    {
        private uint contactId;
        public DeletePhoneView(uint _contactId)
        {
            contactId = _contactId;
        }
        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input phone's id to delete: ");
            bool isPhoneIdNumber = uint.TryParse(Console.ReadLine(), out uint phoneId);

            if (!isPhoneIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var phoneFromInput = new Phone(contactId, phoneId);

            var phoneRepository = new PhoneRepository();
            phoneFromInput = phoneRepository.ReadPhone(phoneFromInput);
            if (phoneFromInput == null)
            {
                Console.WriteLine("Invalid phone id. Phone not found.");
                Console.ReadKey(true);
                return;
            }

            phoneRepository.DeletePhone(phoneFromInput);
        }
    }
}

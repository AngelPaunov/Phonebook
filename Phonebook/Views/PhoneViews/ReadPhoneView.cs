using Phonebook.Entities;
using Phonebook.Repositories;
using System;

namespace Phonebook.Views.PhoneViews
{
    public class ReadPhoneView
    {
        private uint contactId;
        public ReadPhoneView(uint _contactId)
        {
            contactId = _contactId;
        }
        public void Show()
        {
            Console.WriteLine();

            Console.WriteLine("Input phone's id which you want to check.");
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
            

            Console.WriteLine($"ID: {phoneFromInput.Id}");
            Console.WriteLine($"Phone number: {phoneFromInput.PhoneNumber}");
        }
    }
}

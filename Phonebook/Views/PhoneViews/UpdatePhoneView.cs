using Phonebook.Entities;
using Phonebook.Repositories;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class UpdatePhoneView
    {
        private uint contactId;
        private uint userId;
        public UpdatePhoneView(uint _userId, uint _contactId)
        {
            userId = _userId;
            contactId = _contactId;
        }
        public void Show()
        {
            Console.Clear();
            Console.Write("Input phone's id to update:");
            bool isPhoneIdNumber = uint.TryParse(Console.ReadLine(), out uint phoneId);

            if (!isPhoneIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var phoneFromInput = new Phone(userId, contactId, phoneId);

            var phoneRepository = new PhoneRepository();
            phoneFromInput = phoneRepository.ReadPhone(phoneFromInput);
            if (phoneFromInput == null)
            {
                Console.WriteLine("Invalid phone id. Phone not found.");
                Console.ReadKey(true);
                return;
            }

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine();

            if (!phoneNumber.All(c => c >= '0' && c <= '9') || phoneNumber.Length < 8 || phoneNumber.Length > 15)
            {
                Console.WriteLine("Invalid phone number. Please input only positive numbers.");
                return;
            }
            phoneFromInput.PhoneNumber = phoneNumber;

            phoneRepository.UpdatePhone(phoneFromInput);
        }
    }
}

using Phonebook.Entities;
using Phonebook.Repositories;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class CreatePhoneView
    {
        private uint contactId;
        public CreatePhoneView(uint _contactId)
        {
            contactId = _contactId;
        }
        public void Show()
        {
            Console.WriteLine();

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine();

            if (!phoneNumber.All(c => c >= '0' && c <= '9') || phoneNumber.Length < 8 || phoneNumber.Length > 15)
            {
                Console.WriteLine("Invalid phone number. Please input only positive numbers.");
                Console.ReadKey(true);
                return;
            }

            var phoneRepository = new PhoneRepository();
            phoneRepository.CreatePhone(new Phone(contactId, phoneNumber:phoneNumber));
            Console.WriteLine("Successfuly create new phone.");
            Console.ReadKey(true);
        }
    }
}

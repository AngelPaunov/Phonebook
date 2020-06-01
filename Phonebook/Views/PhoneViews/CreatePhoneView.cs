using Phonebook.Entities;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class CreatePhoneView : BasePhoneView
    {
        public CreatePhoneView(IPhoneRepository phoneRepository) : base(phoneRepository)
        { }

        public void Show(uint userId, uint contactId)
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

            phoneRepository.CreatePhone(new Phone(userId, contactId, phoneNumber: phoneNumber));

            Console.WriteLine("Successfuly created new phone.");
            Console.ReadKey(true);
        }
    }
}

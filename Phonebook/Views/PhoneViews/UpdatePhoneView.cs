using Phonebook.Entities;
using Phonebook.CSVRepositories;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class UpdatePhoneView : BasePhoneView
    {
        public UpdatePhoneView(IPhoneRepository phoneRepository, uint userId, uint contactId) : base(phoneRepository, userId, contactId)
        { }

        public void Show()
        {
            Console.Clear();
            Console.Write("Input phone's id to update: ");
            bool isPhoneIdNumber = uint.TryParse(Console.ReadLine(), out uint phoneId);

            if (!isPhoneIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var phoneFromInput = new Phone(userId, contactId, phoneId);

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

            Console.WriteLine("Phone has been updated.");
            Console.ReadKey(true);
        }
    }
}

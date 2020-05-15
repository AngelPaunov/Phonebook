using Phonebook.Entities;
using System;

namespace Phonebook.Views.PhoneViews
{
    public class ReadPhoneView : BasePhoneView
    {
        public ReadPhoneView(IPhoneRepository phoneRepository, uint userId, uint contactId) : base(phoneRepository, userId, contactId)
        { }

        public void Show()
        {
            Console.WriteLine();

            Console.Write("Input phone's id which you want to check: ");
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


            Console.WriteLine($"ID: {phoneFromInput.Id}");
            Console.WriteLine($"Phone number: {phoneFromInput.PhoneNumber}");

            Console.ReadKey(true);
        }
    }
}

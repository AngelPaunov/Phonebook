using Phonebook.Entities;
using System;

namespace Phonebook.Views.PhoneViews
{
    public class ReadPhoneView : BasePhoneView
    {
        public ReadPhoneView(IPhoneRepository phoneRepository) : base(phoneRepository)
        { }

        public void Show(uint userId, uint contactId)
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
            Console.WriteLine($"Create Date: {phoneFromInput.CreateDate.ToLocalTime()}");
            Console.WriteLine($"Update Date: {phoneFromInput.UpdateDate.ToLocalTime()}");

            Console.ReadKey(true);
        }
    }
}

using System;

namespace Phonebook.Views.Phone
{
    public class UpdatePhoneView
    {
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

            //TODO: get phone from db 
            // if phone is null
            Console.WriteLine("Invalid phone id. Phone not found.");
            Console.ReadKey(true);

            //if not null
            Console.Write("Phone number: ");
            bool isNumber = uint.TryParse(Console.ReadLine(), out uint phoneNumber);

            if (!isNumber)
            {
                Console.WriteLine("Invalid phone number. Please input only positive numbers.");
                return;
            }
            //delete the phone in db
        }
    }
}

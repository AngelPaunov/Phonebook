using System;

namespace Phonebook.Views.PhoneViews
{
    public class DeletePhoneView
    {
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

            //TODO: get phone from db 
            // if phone is null
            Console.WriteLine("Invalid phone id. Phone not found.");
            Console.ReadKey(true);

            //if not null
            //update the phone in db
        }
    }
}

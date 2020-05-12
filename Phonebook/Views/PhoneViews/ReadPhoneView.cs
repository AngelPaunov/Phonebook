using System;

namespace Phonebook.Views.PhoneViews
{
    public class ReadPhoneView
    {
        public void Show()
        {
            Console.WriteLine();

            Console.WriteLine("Input phone's id which you want to check.");
            bool isPhoneIdNumber = uint.TryParse(Console.ReadLine(), out uint phoneId);

            if (!isPhoneIdNumber)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey();
                return;
            }

            //TODO: get phone data from db

            //Console.WriteLine($"ID: {phone.id}");
            //Console.WriteLine($"Phone number: {phone.PhoneNumber}");

        }
    }
}

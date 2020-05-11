using System;

namespace Phonebook.Views.Phone
{
    public class CreatePhoneView
    {
        public void Show()
        {
            Console.WriteLine();

            Console.Write("Phone number: ");
            bool isNumber=uint.TryParse(Console.ReadLine(), out uint phoneNumber);

            if(!isNumber)
            {
                Console.WriteLine("Invalid phone number. Please input only positive numbers.");
                return;
            }

            //TODO: write the phone in db
        }
    }
}

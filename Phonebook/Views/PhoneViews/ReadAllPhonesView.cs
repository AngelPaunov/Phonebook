using System;

namespace Phonebook.Views.PhoneViews
{
    public class ReadAllPhonesView
    {
        public void Show()
        {
            Console.Clear();

            //TODO: get all phones from db

            //foreach phone in phones
            //Console.WriteLine($"ID: {phone.Id}");
            //Console.WriteLine($"Phone number: {phone.PhoneNumber}");

            Console.WriteLine();
            Console.ReadKey();
            return;
        }
    }
}

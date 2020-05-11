using System;

namespace Phonebook.Views.Contact
{
    public class ReadAllContactsView
    {
        public void Show()
        {
            Console.Clear();

            //TODO: get all contacts from db

            //foreach contact in contacts
            //Console.WriteLine($"ID: {contact.Id}");
            //Console.WriteLine($"First Name: {contact.FirstName}");
            //Console.WriteLine($"Last Name: {contact.LastName}");
            //Console.WriteLine($"Email: {contact.Email}");

            Console.WriteLine();
            Console.ReadKey();
            return;
        }
    }
}

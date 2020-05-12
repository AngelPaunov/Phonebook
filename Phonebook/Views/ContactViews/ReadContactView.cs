using System;

namespace Phonebook.Views.ContactViews
{
    public class ReadContactView
    {
        public void Show()
        {
            Console.WriteLine();

            Console.WriteLine("Input contact's id which you want to check.");
            bool isContactIdNumber = uint.TryParse(Console.ReadLine(), out uint contactId);

            if (!isContactIdNumber)
            {
                Console.WriteLine("Invalid contact id. Contact not found.");
                Console.ReadKey();
                return;
            }

            //TODO: get contact data from db

            //Console.WriteLine($"ID: {contact.id}");
            //Console.WriteLine($"First Name: {contact.FirstName}");
            //Console.WriteLine($"Last Name: {contact.LastName}");
            //Console.WriteLine($"Email: {contact.Email}");

            //var phoneModifyView = new PhoneModifyView();
            //phoneModifyView.Show();
            //return;
        }
    }
}

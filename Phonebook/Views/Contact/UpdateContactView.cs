using System;

namespace Phonebook.Views.Contact
{
    public class UpdateContactView
    {
        public void Show()
        {
            Console.Clear();
            Console.Write("Input contact's id to update:");
            bool isContactIdNumber = uint.TryParse(Console.ReadLine(), out uint contactId);

            if (!isContactIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            //TODO: get contact from db 
            // if contact is null
            Console.WriteLine("Invalid contact id. Contact not found.");
            Console.ReadKey(true);

            //if not null
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            if (string.IsNullOrEmpty(firstName))
            {
                Console.WriteLine("Invalid first name.");
                return;
            }

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            if (string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("Invalid last name.");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Invalid email.");
                return;
            }

            //update the contact in db
        }
    }
}

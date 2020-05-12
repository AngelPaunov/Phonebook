using System;

namespace Phonebook.Views.ContactViews
{
    public class CreateContactView
    {
        public void Show()
        {
            Console.Clear();

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

            //TODO: save the new contact in db
        }
    }
}

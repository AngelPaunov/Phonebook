using System;

namespace Phonebook.Views.User
{
    public class CreateUserView
    {
        public void Show()
        {
            Console.Clear();
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if(string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Invalid username.");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                Console.Write("Invalid password.");
                return;
            }

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            if (string.IsNullOrEmpty(firstName))
            {
                Console.WriteLine("Invalid first name.");
                return;
            }

            Console.Write("Last Name: ");
            string lastName= Console.ReadLine();

            if (string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("Invalid last name.");
                return;
            }

            Console.Write("Admin (true/false): ");
            bool isAdminValueBool= bool.TryParse(Console.ReadLine(), out bool isAdmin);

            if (!isAdminValueBool)
            {
                Console.WriteLine("Invalid admin value.");
                return;
            }

            //TODO: save the new user in db
        }
    }
}

using System;

namespace Phonebook.Views.ContactViews
{
    public class DeleteContactView
    {
        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input contact's id to delete: ");
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
            //delete the contact in db
        }
    }
}

using System;

namespace Phonebook.Views.User
{
    public class DeleteUserView
    {
        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input user's id to delete: ");
            bool isUserIdNumber = uint.TryParse(Console.ReadLine(), out uint userId);

            if (!isUserIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            //TODO: get user from db 
            // if user is null
            Console.WriteLine("Invalid user id. User not found.");
            Console.ReadKey(true);

            //if not null
            //delete the user in db
        }
    }
}

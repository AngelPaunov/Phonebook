using Phonebook.Entities;
using System;

namespace Phonebook.Views.UserViews
{
    public class UserModifyView
    {
        private readonly IUserRepository userRepository;

        public UserModifyView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                RenderMenu();

                var userChoice = GetChoice();

                if (HandleChoice(userChoice))
                {
                    return;
                }
            }
        }

        private bool HandleChoice(ModifyMenuEnum userChoice)
        {
            switch (userChoice)
            {
                case ModifyMenuEnum.Create:
                    var createUserView = new CreateUserView(userRepository);
                    createUserView.Show();
                    return false;
                case ModifyMenuEnum.ReadAll:
                    var readAllUserView = new ReadAllUsersView(userRepository);
                    readAllUserView.Show();
                    return false;
                case ModifyMenuEnum.ReadSingle:
                    var readUser = new ReadUserView(userRepository);
                    readUser.Show();
                    return false;
                case ModifyMenuEnum.Update:
                    var updateUserView = new UpdateUserView(userRepository);
                    updateUserView.Show();
                    return false;
                case ModifyMenuEnum.Delete:
                    var deleteUserView = new DeleteUserView(userRepository);
                    deleteUserView.Show();
                    return false;
                case ModifyMenuEnum.Exit:
                    return true;
                case ModifyMenuEnum.Invalid:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey(true);
                    return false;
            }

            return false;
        }

        private ModifyMenuEnum GetChoice()
        {
            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.C:
                    return ModifyMenuEnum.Create;
                case ConsoleKey.A:
                    return ModifyMenuEnum.ReadAll;
                case ConsoleKey.R:
                    return ModifyMenuEnum.ReadSingle;
                case ConsoleKey.U:
                    return ModifyMenuEnum.Update;
                case ConsoleKey.D:
                    return ModifyMenuEnum.Delete;
                case ConsoleKey.X:
                    return ModifyMenuEnum.Exit;
                default:
                    return ModifyMenuEnum.Invalid;
            }
        }

        private void RenderMenu()
        {
            Console.WriteLine("[C]reate user");
            Console.WriteLine("Re[a]d all users");
            Console.WriteLine("[R]ead single user");
            Console.WriteLine("[U]pdate user");
            Console.WriteLine("[D]elete user");
            Console.WriteLine("E[x]it");
        }
    }
}

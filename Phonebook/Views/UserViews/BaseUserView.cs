using Phonebook.Entities;
using Phonebook.Repositories.CSV;
using System;

namespace Phonebook.Views.UserViews
{
    public class BaseUserView
    {
        protected IUserRepository _userRepository;

        public BaseUserView(IUserRepository userRepository)
        {
            this._userRepository=userRepository;
        }

        public uint GetIdFromInput()
        {
            bool isUserIdNumber = uint.TryParse(Console.ReadLine(), out uint userInputId);

            Console.WriteLine();

            if (!isUserIdNumber)
            {
                return 0;
            }

            return userInputId;
        }

        public User GetUserById(uint userId) {
            var userFromInput = new User(userId);

            userFromInput = _userRepository.ReadUser(userFromInput);

            return userFromInput;
        }
    }
}

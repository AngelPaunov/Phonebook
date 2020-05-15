using System.Collections.Generic;

namespace Phonebook.Entities
{
    public interface IUserRepository
    {
        void CreateUser(User newUser);

        User ReadUser(User userToRead);

        IEnumerable<User> ReadAllUsers();

        void UpdateUser(User userToUpdate);

        void DeleteUser(User userToDelete);
    }
}

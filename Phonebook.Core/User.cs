using System;
using System.Collections.Generic;

namespace Phonebook.Entities
{
    public class User : BaseEntity
    {
        public User()
        { }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public User(int id, string username, string password, string firstName, string lastName, bool? isAdmin) : this(username, password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
        }
        public User(User user) : this(user.Id, user.Username, user.Password, user.FirstName, user.LastName, user.IsAdmin)
        { }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsAdmin { get; set; }
    }
}

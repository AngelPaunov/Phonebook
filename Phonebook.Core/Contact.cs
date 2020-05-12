namespace Phonebook.Entities
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
        }
        public Contact(uint creatorId, uint contactId, string firstName, string lastName, string email)
        {
            CreatorId = creatorId;
            Id = contactId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public Contact(Contact contact) : this(contact.CreatorId, contact.Id, contact.FirstName, contact.LastName, contact.Email)
        {
        }
        public uint CreatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

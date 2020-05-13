namespace Phonebook.Entities
{
    public class Contact : BaseEntity
    {
        public Contact()
        { }
        public Contact(uint creatorId, uint contactId)
        {
            CreatorId = creatorId;
            Id = contactId;
        }
        public Contact(uint creatorId, string firstName = null, string lastName = null, string email = null, uint contactId = 0) : this(creatorId, contactId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public Contact(Contact contact) : this(contact.CreatorId, contact.FirstName, contact.LastName, contact.Email, contact.Id)
        { }

        public uint CreatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

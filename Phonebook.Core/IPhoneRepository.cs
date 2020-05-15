using System.Collections.Generic;

namespace Phonebook.Entities
{
    public interface IPhoneRepository
    {
        void CreatePhone(Phone newPhone);

        Phone ReadPhone(Phone phoneToRead);

        IEnumerable<Phone> ReadAllPhones();

        void UpdatePhone(Phone phoneToUpdate);

        void DeletePhone(Phone phoneToDelete);
    }
}

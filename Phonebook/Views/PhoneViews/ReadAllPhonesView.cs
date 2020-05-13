using Phonebook.Repositories;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class ReadAllPhonesView
    {
        private uint contactId;
        public ReadAllPhonesView(uint _contactId)
        {
            contactId = _contactId;
        }
        public void Show()
        {
            Console.Clear();

            var phoneRepository = new PhoneRepository();
            foreach (var phone in phoneRepository.ReadAllPhones().Where(c=>c.ContactId==contactId))
            {
                Console.WriteLine($"ID: {phone.Id}");
                Console.WriteLine($"Phone number: {phone.PhoneNumber}");
            }

            Console.WriteLine();
            Console.ReadKey();
            return;
        }
    }
}

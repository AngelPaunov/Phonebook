using Phonebook.Repositories;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class ReadAllPhonesView
    {
        private uint contactId;
        private uint userId;
        public ReadAllPhonesView(uint _userId, uint _contactId)
        {
            userId = _userId;
            contactId = _contactId;
        }
        public void Show()
        {
            Console.Clear();

            var phoneRepository = new PhoneRepository();
            foreach (var phone in phoneRepository.ReadAllPhones().Where(p => p.ContactId == contactId && p.UserId == userId))
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

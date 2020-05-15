using Phonebook.CSVRepositories;
using Phonebook.Entities;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class ReadAllPhonesView : BasePhoneView
    {
        public ReadAllPhonesView(IPhoneRepository phoneRepository, uint userId, uint contactId) : base(phoneRepository, userId, contactId)
        { }

        public void Show()
        {
            Console.Clear();

            var phoneRepository = new CSVPhoneRepository();
            foreach (var phone in phoneRepository.ReadAllPhones().Where(p => p.ContactId == contactId && p.UserId == userId))
            {
                Console.WriteLine($"ID: {phone.Id}");
                Console.WriteLine($"Phone number: {phone.PhoneNumber}");
            }

            Console.WriteLine();
            Console.ReadKey(true);
            return;
        }
    }
}

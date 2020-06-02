using Phonebook.Entities;
using System;
using System.Linq;

namespace Phonebook.Views.PhoneViews
{
    public class ReadAllPhonesView : BasePhoneView
    {
        public ReadAllPhonesView(IPhoneRepository phoneRepository) : base(phoneRepository)
        { }

        public void Show(uint userId, uint contactId)
        {
            Console.Clear();

            foreach (var phone in phoneRepository.ReadAllPhones().Where(p => p.ContactId == contactId && p.UserId == userId))
            {
                Console.WriteLine($"ID: {phone.Id}");
                Console.WriteLine($"Phone number: {phone.PhoneNumber}");
                Console.WriteLine($"Create Date: {phone.CreateDate.ToLocalTime()}");
                Console.WriteLine($"Update Date: {phone.UpdateDate.ToLocalTime()}");
            }

            Console.WriteLine();
            Console.ReadKey(true);
            return;
        }
    }
}

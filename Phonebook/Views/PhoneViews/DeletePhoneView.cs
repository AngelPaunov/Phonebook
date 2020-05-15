﻿using Phonebook.Entities;
using System;

namespace Phonebook.Views.PhoneViews
{
    public class DeletePhoneView : BasePhoneView
    {
        public DeletePhoneView(IPhoneRepository phoneRepository, uint userId, uint contactId) : base(phoneRepository, userId, contactId)
        { }

        public void Show()
        {
            Console.WriteLine();
            Console.Write("Input phone's id to delete: ");
            bool isPhoneIdNumber = uint.TryParse(Console.ReadLine(), out uint phoneId);

            if (!isPhoneIdNumber)
            {
                Console.WriteLine("Please input positive number.");
                Console.ReadKey();
                return;
            }

            var phoneFromInput = new Phone(userId, contactId, phoneId);

            phoneFromInput = phoneRepository.ReadPhone(phoneFromInput);
            if (phoneFromInput == null)
            {
                Console.WriteLine("Invalid phone id. Phone not found.");
                Console.ReadKey(true);
                return;
            }

            phoneRepository.DeletePhone(phoneFromInput);
            Console.WriteLine("Phone has been deleted.");
            Console.ReadKey(true);
        }
    }
}

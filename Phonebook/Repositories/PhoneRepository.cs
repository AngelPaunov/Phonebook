using Phonebook.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phonebook.Repositories
{
    public class PhoneRepository
    {
        private readonly string filePath = "Phones.txt";

        public PhoneRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void CreatePhone(Phone newPhone)
        {
            Phone lastPhone = ReadAllPhones().LastOrDefault();

            if (lastPhone == null)
            {
                newPhone.Id = 1;
            }
            else
            {
                newPhone.Id = lastPhone.Id + 1;
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{newPhone.ContactId},{newPhone.Id},{newPhone.PhoneNumber}");
            }
        }

        public Phone ReadPhone(Phone phoneToRead)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var phone = GetPhoneFromCSVLine(reader.ReadLine());

                    if (phone.Id == phoneToRead.Id)
                    {
                        return phone;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Phone> ReadAllPhones()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var phone = GetPhoneFromCSVLine(reader.ReadLine());

                    yield return phone;
                }
            }
        }

        public void UpdatePhone(Phone phoneToUpdate)
        {
            string temporaryFilePath = "temporaryFile.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (StreamWriter writer = new StreamWriter(temporaryFilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var phone = GetPhoneFromCSVLine(reader.ReadLine());

                        if (phone.Id == phoneToUpdate.Id && phone.ContactId == phoneToUpdate.ContactId)
                        {
                            writer.WriteLine($"{phoneToUpdate.ContactId},{phoneToUpdate.Id},{phoneToUpdate.PhoneNumber}");
                            continue;
                        }

                        writer.WriteLine($"{phone.ContactId},{phone.Id},{phone.PhoneNumber}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        public void DeletePhone(Phone phoneToDelete)
        {
            string temporaryFilePath = "temporaryFile.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                using (StreamWriter writer = new StreamWriter(temporaryFilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var phone = GetPhoneFromCSVLine(reader.ReadLine());

                        if (phone.Id == phoneToDelete.Id)
                        {
                            continue;
                        }

                        if (phone.ContactId == phoneToDelete.ContactId && phone.Id > phoneToDelete.Id)
                        {
                            writer.WriteLine($"{phone.ContactId},{phone.Id - 1},{phone.PhoneNumber}");
                            continue;
                        }

                        writer.WriteLine($"{phone.ContactId},{phone.Id},{phone.PhoneNumber}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        private Phone GetPhoneFromCSVLine(string line)
        {
            string[] phoneData = line.Split(',');

            uint contactId = uint.Parse(phoneData[0]);
            uint phoneId = uint.Parse(phoneData[1]);
            string phoneNumber = phoneData[2];

            return new Phone(contactId,phoneId,phoneNumber);
        }
    }
}

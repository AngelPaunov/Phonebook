using Phonebook.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.CSVRepositories
{
    public class CSVPhoneRepository : IPhoneRepository
    {
        private readonly string filePath = "Phones.txt";

        public CSVPhoneRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void CreatePhone(Phone newPhone)
        {
            Phone lastPhone = ReadAllPhones().LastOrDefault(p => p.UserId == newPhone.UserId && p.ContactId == newPhone.ContactId);

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
                writer.WriteLine($"{newPhone.UserId},{newPhone.ContactId},{newPhone.Id},{newPhone.PhoneNumber}");
            }
        }

        public Phone ReadPhone(Phone phoneToRead)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var phone = GetPhoneFromCSVLine(reader.ReadLine());

                    if (phone.Id == phoneToRead.Id && phone.UserId == phoneToRead.UserId && phone.ContactId == phoneToRead.ContactId)
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

                        if (phone.Id == phoneToUpdate.Id && phone.UserId == phoneToUpdate.UserId && phone.ContactId == phoneToUpdate.ContactId)
                        {
                            writer.WriteLine($"{phoneToUpdate.UserId},{phoneToUpdate.ContactId},{phoneToUpdate.Id},{phoneToUpdate.PhoneNumber}");
                            continue;
                        }

                        writer.WriteLine($"{phone.UserId},{phone.ContactId},{phone.Id},{phone.PhoneNumber}");
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

                        if (phone.Id == phoneToDelete.Id && (phone.UserId == phoneToDelete.UserId && phone.ContactId == phoneToDelete.ContactId))
                        {
                            continue;
                        }

                        if ((phone.ContactId == phoneToDelete.ContactId && phone.UserId == phoneToDelete.UserId) && phone.Id > phoneToDelete.Id)
                        {
                            writer.WriteLine($"{phone.UserId},{phone.ContactId},{phone.Id},{phone.PhoneNumber}");
                            continue;
                        }

                        writer.WriteLine($"{phone.UserId},{phone.ContactId},{phone.Id},{phone.PhoneNumber}");
                    }
                }
            }
            File.Move(temporaryFilePath, filePath, true);
        }

        private Phone GetPhoneFromCSVLine(string line)
        {
            string[] phoneData = line.Split(',');

            uint userId = uint.Parse(phoneData[0]);
            uint contactId = uint.Parse(phoneData[1]);
            uint phoneId = uint.Parse(phoneData[2]);
            string phoneNumber = phoneData[3];

            return new Phone(userId, contactId, phoneId, phoneNumber);
        }
    }
}

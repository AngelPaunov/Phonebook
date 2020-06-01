using Newtonsoft.Json;
using Phonebook.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.Repositories.JSON
{
    public class JSONPhoneRepository : IPhoneRepository
    {
        private readonly string filePath = "phones.json";

        public JSONPhoneRepository()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void CreatePhone(Phone newPhone)
        {
            var jsonString = File.ReadAllText(filePath);

            var phonesList = JsonConvert.DeserializeObject<List<Phone>>(jsonString) ?? new List<Phone>();
            var lastphone = phonesList.LastOrDefault();

            newPhone.Id = lastphone == null ? 1 : lastphone.Id + 1;
            phonesList.Add(newPhone);

            var jsonFinalFile = JsonConvert.SerializeObject(phonesList, Formatting.Indented);
            File.WriteAllText(filePath, jsonFinalFile);
        }

        public void DeletePhone(Phone phoneToDelete)
        {
            string jsonString = File.ReadAllText(filePath);
            var phoneList = JsonConvert.DeserializeObject<Phone[]>(jsonString).Where(p => p.Id != phoneToDelete.Id);
            var jsonFinalFile = JsonConvert.SerializeObject(phoneList, Formatting.Indented);

            File.WriteAllText(filePath, jsonFinalFile);
        }

        public IEnumerable<Phone> ReadAllPhones()
        {
            var jsonString = File.ReadAllText(filePath);
            var phonesList = JsonConvert.DeserializeObject<Phone[]>(jsonString);

            return phonesList;
        }

        public Phone ReadPhone(Phone phoneToRead)
        {
            var jsonString = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<Phone[]>(jsonString)?.FirstOrDefault(p => p.Id == phoneToRead.Id);
        }

        public void UpdatePhone(Phone phoneToUpdate)
        {
            string jsonString = File.ReadAllText(filePath);
            var phoneList = JsonConvert.DeserializeObject<Phone[]>(jsonString);
            var phoneIndex = phoneToUpdate.Id - 1;
            phoneList[phoneIndex] = new Phone(phoneToUpdate);

            var jsonFinalFile = JsonConvert.SerializeObject(phoneList, Formatting.Indented);
            File.WriteAllText(filePath, jsonFinalFile);
        }
    }
}

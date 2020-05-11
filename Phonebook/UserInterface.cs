using Phonebook.Data;
using Phonebook.Entities;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phonebook
{
    public static class UserInterface
    {
        private static int UserId { get; set; }
        private static int ContactId { get; set; }

        public static ConsoleKey HomeViewModel()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to our app");
            Console.WriteLine("[L]ogin");
            Console.WriteLine("[E]xit");
            return Console.ReadKey(true).Key;
        }

        public static User LoginViewModel(BaseRepository<User> userRepository)
        {
            Console.Clear();

            string username, password = "";

            Console.WriteLine($"Input your credentials");
            Console.Write($"Username: ");
            username = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            Console.Write($"Password: ");
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            bool loggedIn = userRepository.UserAuthentication(new User(username, password));
            if (loggedIn)
            {
                User loggedInUser = userRepository.GetUser(username);
                UserId = loggedInUser.Id;
                return loggedInUser;
            }
            else
                return null;
        }

        public static void AdminMenu(IBaseRepository<User> userRepository, IBaseRepository<Contact> contactRepository)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("To operate with users press [U] for contacts [C]. To log out press ESC");
                ConsoleKey step = Console.ReadKey(true).Key;

                if (step == ConsoleKey.Escape)
                    break;

                while (step == ConsoleKey.U && step != ConsoleKey.Escape)
                {
                    step = ModifyEntity(userRepository);
                }
                while (step == ConsoleKey.C && step != ConsoleKey.Escape)
                {
                    step = ModifyEntity(contactRepository);
                }
            }
        }

        public static void UserMenu(IBaseRepository<Contact> contactRepository)
        {
            while (true)
            {
                ConsoleKey step;
                do
                {
                    step = ModifyEntity(contactRepository);
                }
                while (step == ConsoleKey.C && step != ConsoleKey.Escape);

                if (step == ConsoleKey.Escape)
                    break;
            }
        }

        public static ConsoleKey ModifyEntity<T>(IBaseRepository<T> entityRepository) where T : BaseEntity, new()
        {
            string typeNameToLower = typeof(T).Name.ToLower();

            if (typeNameToLower == "phone")
            {
                Console.WriteLine();
                Console.WriteLine($"Command list for {typeNameToLower}: \n Create \n Edit [id] \n Delete [id]");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Command list for {typeNameToLower}: \n Create \n Edit [id] \n Delete [id] \n Get all \n Get [id]");
            }

            Console.WriteLine($"Press ESC to return or press any key to continue");
            ConsoleKey step = Console.ReadKey(true).Key;
            if (step == ConsoleKey.Escape)
                return ConsoleKey.Escape;

            Console.Write("Write the desired command: ");
            string[] commandData = Regex.Split(Console.ReadLine().Trim(), @"\s+");
            string command = commandData[0];

            string message = null;
            int entityId = 0;
            int databaseEntityIndex = 0;

            GetSecondPartOfCommand(entityRepository, typeNameToLower, commandData, ref command, ref entityId, ref databaseEntityIndex);

            if (databaseEntityIndex == 0 && entityId == 0 && command.ToLower() != "create")
            {
                message = "Invalid id";
                command = "";
            }

            message = ExecuteCommand(entityRepository, typeNameToLower, command, message, entityId, databaseEntityIndex);

            Console.WriteLine(message);
            Console.WriteLine(Environment.NewLine + "Press ESC to return or press any key to continue"); //TODO: remove the second press ESC to return so it doesn't log out but return to contact
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                return ConsoleKey.Escape;

            if (typeNameToLower == "user")
                return ConsoleKey.U;
            else if (typeNameToLower == "contact")
                return ConsoleKey.C;
            else return ConsoleKey.P;
        }

        private static string ExecuteCommand<T>(IBaseRepository<T> entityRepository, string typeNameToLower, string command, string message, int entityId, int databaseEntityIndex) where T : BaseEntity, new()
        {
            switch (command.ToLower())
            {
                case "create":
                    message = entityRepository.CreateNewEntity(InputEntityData<T>()) ? $"Successfully created new {typeNameToLower}." : $"Invalid {typeNameToLower} data.";
                    break;
                case "edit":
                    message = entityRepository.EditEntity(InputEntityData<T>(), databaseEntityIndex) ? $"Successfully edited {typeNameToLower}." : $"Invalid {typeNameToLower} data.";
                    break;
                case "delete":
                    bool deleteEntity = entityRepository.DeleteEntity(databaseEntityIndex);

                    if (typeNameToLower != "phone")
                        DeleteHierarchy(typeNameToLower, entityId, databaseEntityIndex, deleteEntity);

                    message = deleteEntity ? $"Successfully deleted {typeNameToLower}." : $"Invalid {typeNameToLower} data or id.";
                    break;
                case "get":
                    if (typeNameToLower != "phone")
                        DisplayEntity(entityRepository.GetEntity(databaseEntityIndex), entityId, true);
                    break;
                case "getall":
                    if (typeNameToLower != "phone")
                        DisplayEntities(entityRepository.GetAllEntities());
                    break;
            }

            return message;
        }

        private static void GetSecondPartOfCommand<T>(IBaseRepository<T> entityRepository, string typeNameToLower, string[] commandData, ref string command, ref int entityId, ref int databaseEntityIndex) where T : BaseEntity, new()
        {
            if (commandData.Length >= 2)
            {
                entityId = int.TryParse(commandData[1], out int id) ? id : -1;

                if (entityId == -1)
                    command += commandData[1];
                else
                {
                    try
                    {
                        int entityIdd = entityId;
                        if (typeNameToLower == "contact")
                            databaseEntityIndex = entityRepository.GetAllEntities().Where(c => (c as Contact).CreatorId == UserId).OrderBy(c => c.Id).ElementAt(entityId - 1).Id;
                        else if (typeNameToLower == "phone")
                            databaseEntityIndex = entityRepository.GetAllEntities().Where(c => (c as Phone).ContactId == ContactId).OrderBy(c => c.Id).ElementAt(entityId - 1).Id;
                        else
                            databaseEntityIndex = entityRepository.GetAllEntities().First(c => c.Id == entityIdd).Id;
                    }
                    catch (Exception)
                    {
                        entityId = 0;
                    }
                }
            }
        }

        private static void DeleteHierarchy(string typeNameToLower, int entityId, int databaseEntityIndex, bool deleteEntity)
        {
            var phoneRepository = new BaseRepository<Phone>();

            if (typeNameToLower == "user" && deleteEntity)
            {
                var contactRepository = new BaseRepository<Contact>();

                Contact[] contacts = contactRepository.GetAllEntities().Where(c => c.CreatorId == entityId).ToArray();
                for (int i = 0; i < contacts.Count(); i++)
                {
                    contactRepository.DeleteEntity(contacts[i].CreatorId);
                    DeletePhonesByContactId(phoneRepository, contacts[i].Id);
                }
            }

            if (typeNameToLower == "contact" && deleteEntity)
                DeletePhonesByContactId(phoneRepository, databaseEntityIndex);
        }

        private static void DeletePhonesByContactId(BaseRepository<Phone> phoneRepository, int contactId)
        {
            Phone[] phones = phoneRepository.GetAllEntities().Where(c => c.ContactId == contactId).ToArray();
            for (int i = 0; i < phones.Count(); i++)
            {
                phoneRepository.DeleteEntity(phones[i].Id);
            }
        }

        private static void DisplayEntity(BaseEntity entityToDisplay, int displayId, bool phoneMenu)
        {
            string entityTypeToLower = entityToDisplay?.GetType().Name.ToLower();

            if (entityToDisplay == null || (entityTypeToLower == "contact" && (entityToDisplay as Contact).CreatorId != UserId))
            {
                Console.WriteLine(Environment.NewLine + "Invalid id");
                return;
            }

            if (entityTypeToLower != "phone")
                Console.WriteLine();

            foreach (var property in entityToDisplay.GetType().GetProperties())
            {
                string propertyNameToLower = property.Name.ToLower();

                if (entityTypeToLower == "contact" && propertyNameToLower == "id")
                {
                    ContactId = (int)property.GetValue(entityToDisplay);
                    Console.WriteLine($"{property.Name}: {displayId}");
                    continue;
                } //Set the contact id and display it
                if (entityTypeToLower == "phone" && propertyNameToLower == "phonenumber")
                {
                    Console.WriteLine($"{property.Name} {displayId}: {property.GetValue(entityToDisplay, null)}");
                    continue;
                } // Display phone numbers in different way than the others - Phonenumber [id]: [phonenumber]
                if (propertyNameToLower == "creatorid" || (entityTypeToLower == "phone" && (propertyNameToLower == "contactid" || propertyNameToLower == "id"))) // Don't display certain properties
                    continue;
                if (propertyNameToLower == "createdate" || propertyNameToLower == "updatedate")
                {
                    DateTimeOffset utcTime = new DateTimeOffset(DateTime.Parse(property.GetValue(entityToDisplay).ToString()));
                    Console.WriteLine($"{property.Name.Substring(0, 6)} {property.Name.Substring(6)}: {utcTime.LocalDateTime.ToString("G", CultureInfo.CreateSpecificCulture("en-US"))}");
                    continue;
                }

                Console.WriteLine($"{property.Name}: {property.GetValue(entityToDisplay, null)}");
            }


            if (entityTypeToLower == "contact" && (entityToDisplay as Contact).Id == ContactId)
            {
                DisplayEntities(new BaseRepository<Phone>().GetAllEntities());

                //ConsoleKey step = ConsoleKey.P;
                if (phoneMenu)//step == ConsoleKey.P &&
                {
                    ModifyEntity(new BaseRepository<Phone>());
                }
            }
        }

        private static void DisplayEntities(IEnumerable entitiesToDisplay)
        {
            int index = 1;
            foreach (BaseEntity entity in entitiesToDisplay)
            {
                string entityTypeToLower = entity.GetType().Name.ToLower();

                if (entityTypeToLower == "contact" && (entity as Contact).CreatorId != UserId)
                    continue;
                if (entityTypeToLower == "phone" && (entity as Phone).ContactId != ContactId)
                    continue;

                DisplayEntity(entity, index, false);
                index++;
            }
        }

        private static T InputEntityData<T>() where T : BaseEntity, new()
        {
            string typeNameToLower = typeof(T).Name.ToLower();

            if (typeNameToLower != "phone")
                Console.Clear();

            T entity = new T();
            foreach (var property in entity.GetType().GetProperties())
            {
                string propertyNameToLower = property.Name.ToLower();

                if (propertyNameToLower == "createdate" || propertyNameToLower == "updatedate")
                    continue;
                else if (propertyNameToLower == "contactid" || (propertyNameToLower == "id" && typeNameToLower != "user"))
                {
                    property.SetValue(entity, ContactId);
                    continue;
                }
                else if ((typeNameToLower == "contact" && propertyNameToLower == "creatorid") || (typeNameToLower == "user" && propertyNameToLower == "id"))
                {
                    property.SetValue(entity, UserId);
                    continue;
                }

                Console.Write($"{property.Name}: ");

                if (propertyNameToLower == "isadmin")
                {
                    bool.TryParse(Console.ReadLine().Trim(), out bool isAdmin);
                    property.SetValue(entity, isAdmin);
                }
                else
                    property.SetValue(entity, Console.ReadLine().Trim());
            }
            return entity;
        }
    }
}

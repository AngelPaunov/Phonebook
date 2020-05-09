using Phonebook.Entities;
using System.Collections.Generic;

namespace Phonebook.Data
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetEntity(int entityId);
        IEnumerable<T> GetAllEntities();
        bool EditEntity(T entityToSave, int entityId);
        bool CreateNewEntity(T entityToCreate);
        bool DeleteEntity(int entityId);
    }
}
using Phonebook.Entities;
using System.Collections.Generic;

namespace Phonebook.Data
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetEntity(uint entityId);
        IEnumerable<T> GetAllEntities();
        bool EditEntity(T entityToSave, uint entityId);
        bool CreateNewEntity(T entityToCreate);
        bool DeleteEntity(uint entityId);
    }
}
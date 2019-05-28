using RESTApiTODOList.Core.Domain;
using System.Collections.Generic;

namespace RESTApiTODOList.Core.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        T Get(int Id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}

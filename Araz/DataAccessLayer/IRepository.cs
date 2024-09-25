using System.Collections.Generic;

namespace Models
{
    interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> GetById(int id);
        void Update(T entity);
        void Add(T entity);
        void Delete(T entity);

    }
}

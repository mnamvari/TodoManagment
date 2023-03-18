using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoManagement.Appliaction.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Get(int id);
        Task<IList<T>> GetAll();
        void Update(T entity);
        void Delete(T entity);
        bool Exists(int id);
    }
}

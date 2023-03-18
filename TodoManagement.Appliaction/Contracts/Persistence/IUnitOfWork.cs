using System;

namespace TodoManagement.Appliaction.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoRepository Todos
        {
            get;
        }
        int SaveChanges();
    }
}

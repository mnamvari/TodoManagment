using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Persistence.Repositories;

namespace TodoManagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoManagementDbContext _dbContext;

        public ITodoRepository Todos { get; private set; }

        public UnitOfWork(TodoManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            Todos = new TodoRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}

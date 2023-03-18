using System;
using System.Collections.Generic;
using System.Text;
using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Domains;

namespace TodoManagement.Persistence.Repositories
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        private readonly TodoManagementDbContext _dbContext;
        public TodoRepository(TodoManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

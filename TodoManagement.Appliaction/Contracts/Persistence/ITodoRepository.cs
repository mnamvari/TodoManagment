using System;
using System.Collections.Generic;
using System.Text;
using TodoManagement.Domains;

namespace TodoManagement.Appliaction.Contracts.Persistence
{
    public interface  ITodoRepository:IGenericRepository<Todo> 
    {

    }
}

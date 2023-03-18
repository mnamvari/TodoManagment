using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoManagement.Domains;
using TodoManagement.Persistence.Configuration.Entities;

namespace TodoManagement.Persistence
{
    public class TodoManagementDbContext : DbContext
    {
        public TodoManagementDbContext(DbContextOptions<TodoManagementDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }

        public DbSet<Todo> Todos { get; set; }

    }
}

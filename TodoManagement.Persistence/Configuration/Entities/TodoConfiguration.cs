using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManagement.Domains;

namespace TodoManagement.Persistence.Configuration.Entities
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasData(
                new Todo
                {
                    Id = 1,
                    Title = "Complete project",
                    Description = "Finish project report and presentation",
                    DueDate = DateTime.Now,
                },
                new Todo
                {
                    Id = 2,
                    Title = "Call client",
                    Description = "Follow up with client on project status",
                    DueDate = DateTime.Now,
                },
                new Todo
                {
                    Id = 3,
                    Title = "Prepare for meeting",
                    Description = "Research and prepare for project meeting",
                    DueDate = DateTime.Now,
                }
                );
        }
    }
}

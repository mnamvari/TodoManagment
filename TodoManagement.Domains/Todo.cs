using System;

namespace TodoManagement.Domains
{
    public class Todo
    {
        public int Id { get; set; }
        
        //[MaxLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}

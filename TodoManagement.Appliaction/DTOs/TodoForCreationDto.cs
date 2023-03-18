using System;
using System.Collections.Generic;
using System.Text;

namespace TodoManagement.Appliaction.DTOs
{
    public class TodoForCreationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}

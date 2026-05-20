using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatues Status { get; set; }
        public TaskPriority Priority { get; set; }

        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

    }
}

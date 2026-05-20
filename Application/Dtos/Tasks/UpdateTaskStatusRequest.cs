using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Tasks
{
    public class UpdateTaskStatusRequest
    {
        public TaskStatues Status { get; set; }
    }
}

using Application.Dtos.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponse>CreateAsync(CreateTaskRequest request);
        Task<IEnumerable<TaskResponse>> GetByProjectIdAsync(int projectId);
        Task<TaskResponse> UpdateStatuesAsync(int taskId, UpdateTaskStatusRequest request);
        Task<bool> DeleteAsync(int taskId);
    }
}

using Application.Dtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResponse>CreateAsync(CreateProjectRequest request);
        Task<IEnumerable<ProjectResponse>>GetAllAsync();
        Task<ProjectResponse>GetByIdAsync(int id);
        Task<ProjectResponse>UpdateAsync(int id, UpdateProjectRequest request);
        Task<bool>DeleteAsync(int id);
    }
}

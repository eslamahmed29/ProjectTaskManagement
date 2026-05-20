using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dtos.Projects;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTask.Extensions;

namespace ProjectTask.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;

        public ProjectsController(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        

        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationResponse());
            }
            var result = await _projectService.CreateAsync(request);
            if (result == null)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Failed to create project"));
            }
            return Ok(ApiResponse<ProjectResponse>.SuccessResponse(result, "Project created successfully"));
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectService.GetAllAsync();
            if (result == null || !result.Any())
            {
                return NotFound(ApiResponse<string>.FailureResponse("No projects found"));
            }
            return Ok(ApiResponse<IEnumerable<ProjectResponse>>.SuccessResponse(result, "Projects retrieved successfully"));
        }

        [HttpGet("GetProjectById/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var result = await _projectService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Project not found"));
            }
            return Ok(ApiResponse<ProjectResponse>.SuccessResponse(result, "Project retrieved successfully"));
        }

        [HttpPut("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationResponse());
            }
            var result = await _projectService.UpdateAsync(id, request);
            if (result == null)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Project not found"));
            }
            return Ok(ApiResponse<ProjectResponse>.SuccessResponse(result, "Project updated successfully"));
        }

        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Project not found"));
            }
            return NoContent();
        }
    }
}

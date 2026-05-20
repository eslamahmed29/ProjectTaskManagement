using Application.Common.Models;
using Application.Dtos.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTask.Extensions;

namespace ProjectTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationResponse());
            }
            var result = await _taskService.CreateAsync(request);

            if (result == null)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Task not found"));
            }
            return Ok(ApiResponse<TaskResponse>.SuccessResponse(result, "Task created successfully"));
        }
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var result = await _taskService.GetByProjectIdAsync(projectId);
            if (result == null || !result.Any())
            {
                return NotFound(ApiResponse<string>.FailureResponse("Project not found"));
            }
            return Ok(ApiResponse<IEnumerable<TaskResponse>>.SuccessResponse(result, "Tasks retrieved successfully"));
        }
        [HttpPut("{taskId}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, [FromBody] UpdateTaskStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationResponse());
            }
            var result = await _taskService.UpdateStatuesAsync(taskId, request);
            if (result == null)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Task not found"));
            }
            return Ok(ApiResponse<TaskResponse>.SuccessResponse(result, "Task status updated successfully"));
        }
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var result = await _taskService.DeleteAsync(taskId);
            if (!result)
            {
                return NotFound(ApiResponse<string>.FailureResponse("Task not found"));
            }
            return NoContent();
        }
    }
}

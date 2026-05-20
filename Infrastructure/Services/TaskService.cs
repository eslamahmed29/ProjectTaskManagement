using Application.Common.Interfaces;
using Application.Dtos.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public TaskService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
        {
            var project =await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId && p.UserId == _currentUserService.UserId);
            if(project == null)
            {
                return null;
            }
            var taskItem = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate ?? DateTime.Now.AddDays(7),
                Priority = request.Priority,
                Status = TaskStatues.pending,
                ProjectId = request.ProjectId
            };
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
            return new TaskResponse
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                DueDate = taskItem.DueDate,
                Priority = taskItem.Priority,
                Status = taskItem.Status,
                ProjectId = taskItem.ProjectId
            };
        }

        public async Task<bool> DeleteAsync(int taskId)
        {
            var taskItem = await _context.TaskItems.Include(t=>t.Project).FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == _currentUserService.UserId);
            if (taskItem == null)
            {
                return false;
            }
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskResponse>> GetByProjectIdAsync(int projectId)
        {
            var project = await _context.Projects.AnyAsync(p => p.Id == projectId && p.UserId == _currentUserService.UserId);
            if (!project)
            {
                return Enumerable.Empty<TaskResponse>();
            }

            var tasks = await _context.TaskItems
                .Where(t => t.ProjectId == projectId)
                .Select(t => new TaskResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status,
                    ProjectId = t.ProjectId
                })
                .ToListAsync();

            return tasks;

        }

        public async Task<TaskResponse> UpdateStatuesAsync(int taskId, UpdateTaskStatusRequest request)
        {
            var taskItem = await _context.TaskItems.Include(t=>t.Project)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == _currentUserService.UserId);
            if (taskItem == null)
            {
                return null;
            }
            taskItem.Status = request.Status;
            await _context.SaveChangesAsync();
            return new TaskResponse
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                DueDate = taskItem.DueDate,
                Priority = taskItem.Priority,
                Status = taskItem.Status,
                ProjectId = taskItem.ProjectId
            };
        }
    }
}

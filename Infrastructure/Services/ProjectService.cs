using Application.Common.Interfaces;
using Application.Dtos.Projects;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _CurrentUserService;
        public ProjectService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _CurrentUserService = currentUserService;
        }
        public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = _CurrentUserService.UserId!
            };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project =await _context.Projects
                .Where(p => p.UserId == _CurrentUserService.UserId && p.Id == id)
                .FirstOrDefaultAsync();
            if (project == null)
            {
                return false;
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
        {
            var userId = _CurrentUserService.UserId;
            var projects =await _context.Projects
                .Where(p => p.UserId == userId)
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                }).ToListAsync();

            return projects;
        }

        public async Task<ProjectResponse> GetByIdAsync(int id)
        {
            var project = await _context.Projects
                .Where(p => p.UserId == _CurrentUserService.UserId && p.Id == id)
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                }).FirstOrDefaultAsync();

            return project;
        }

        public async Task<ProjectResponse> UpdateAsync(int id, UpdateProjectRequest request)
        {
            var project = await _context.Projects
                .Where(p => p.UserId == _CurrentUserService.UserId && p.Id == id)
                .FirstOrDefaultAsync();
            if(project == null)
            {
                return null;
            }
            project.Name = request.Name;
            project.Description = request.Description;
            await _context.SaveChangesAsync();
            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }
    }
}

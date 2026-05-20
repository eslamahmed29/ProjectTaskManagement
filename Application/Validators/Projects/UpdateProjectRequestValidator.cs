using Application.Dtos.Projects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Projects
{
    public class UpdateProjectRequestValidator:AbstractValidator<UpdateProjectRequest>
    {
        public UpdateProjectRequestValidator() 
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MinimumLength(3).WithMessage("Project name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Project name must be at most 50 characters long.");
            RuleFor(x=>x.Description)
                .NotEmpty().WithMessage("Project description is required.")
                .MaximumLength(500).WithMessage("Project description must be at most 500 characters long.");
        }
    }
}

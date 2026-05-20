using Application.Dtos.Projects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Projects
{
    public class CreateProjectRequestValidator:AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidator() 
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MinimumLength(3).WithMessage("Project name must be at least 3 characters long.");
            RuleFor(x=>x.Description)
                .NotEmpty().WithMessage("Project description is required.")
                .MinimumLength(10).WithMessage("Project description must be at least 10 characters long.");
           
        }
    }
}

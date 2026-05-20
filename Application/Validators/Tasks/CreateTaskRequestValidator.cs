using Application.Dtos.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Tasks
{
    public class CreateTaskRequestValidator:AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator() 
        {
            RuleFor(x=>x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
            RuleFor(x=>x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x=>x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");
                RuleFor(x=>x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be greater than 0.");
        }
    }
}

using Application.Dtos.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Tasks
{
    public class UpdateTaskStatusRequestValidator : AbstractValidator<UpdateTaskStatusRequest>
    {
        public UpdateTaskStatusRequestValidator() {
           
            RuleFor(x => x.Status)
                
                .IsInEnum().WithMessage("Invalid status value.");
        }
    }
}

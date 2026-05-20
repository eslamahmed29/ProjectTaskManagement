using Application.Common.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProjectTask.Extensions
{
    public static class ValidationExtensions
    {
        public static ApiResponse<string> ToValidationResponse(this ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();
            return ApiResponse<string>.FailureResponse("Validation failed", errors);
        }
    }
}

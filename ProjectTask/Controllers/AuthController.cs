using Application.Common.Models;
using Application.Dtos.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProjectTask.Extensions;

namespace ProjectTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationResponse());
            }
            var result = await _authService.RegisterAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<AuthResponse>.FailureResponse(result.Message));
            }
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, result.Message));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState.ToValidationResponse());
            }
            var result = await _authService.LoginAsync(request);
            if (!result.IsSuccess)
            {
                return Unauthorized(ApiResponse<AuthResponse>.FailureResponse(result.Message));
            }
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, result.Message));
        }
    }
}

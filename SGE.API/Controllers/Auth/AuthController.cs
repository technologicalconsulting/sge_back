using Microsoft.AspNetCore.Mvc;
using SGE.Application.Auth.DTOs;
using SGE.Application.Auth.Interfaces;

namespace SGE.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (response == null) return Unauthorized("Credenciales inválidas o usuario bloqueado");
            return Ok(response);
        }
    }
}

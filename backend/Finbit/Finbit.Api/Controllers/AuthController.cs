using Finbit.Api.Auth.Interfaces;
using Finbit.Api.Auth.Models;
using Finbit.Api.Auth.Interfaces;
using Finbit.Api.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finbit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthRequest request)
    {
        if (request.Username == "admin" && request.Password == "1234")
        {
            var token = _jwtTokenService.GenerateToken(request.Username, "Admin");

            var response = new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            return Ok(response);
        }
        else if (request.Username == "user" && request.Password == "1234")
        {
            var token = _jwtTokenService.GenerateToken(request.Username, "User");

            var response = new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            return Ok(response);
        }

        return Unauthorized("Invalid username or password.");
    }

}

namespace Finbit.Api.Auth.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(string username, string role);
}

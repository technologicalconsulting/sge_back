using SGE.Application.Auth.DTOs;

namespace SGE.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}

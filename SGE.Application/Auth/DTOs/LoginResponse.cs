namespace SGE.Application.Auth.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public List<MenuModulo> Menu { get; set; } = new();
    }
}
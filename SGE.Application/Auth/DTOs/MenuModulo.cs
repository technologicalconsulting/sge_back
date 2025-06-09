namespace SGE.Application.Auth.DTOs
{
    public class MenuModulo
    {
        public string Modulo { get; set; } = string.Empty;
        public List<SubModulo> Submodulos { get; set; } = new();
    }
}
namespace User.Application.DTOs
{
    /// <summary>
    /// Промежуточный DTO между Grpc и Application
    /// </summary>
    public class LoginRequestDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

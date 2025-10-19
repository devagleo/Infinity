namespace User.Application.DTOs
{
    /// <summary>
    /// Промежуточный DTO между Grpc и Application
    /// </summary>
    public class RegisterRequestDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

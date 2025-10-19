namespace User.Application.DTOs
{    
    /// <summary>
    /// Промежуточный DTO между Grpc и Application
    /// </summary>
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
    }
}

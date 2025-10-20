namespace Gateway.Api.Models.User
{
    /// <summary>
    /// Транспортная модель между API и GRPC клиентом
    /// </summary>
    public class RegisterRequestModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

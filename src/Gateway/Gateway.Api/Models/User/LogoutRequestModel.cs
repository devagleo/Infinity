namespace Gateway.Api.Models.User
{
    /// <summary>
    /// Транспортная модель между API и GRPC клиентом
    /// </summary>
    public class LogoutRequestModel
    {
        public string Token { get; set; } = string.Empty;
    }
}

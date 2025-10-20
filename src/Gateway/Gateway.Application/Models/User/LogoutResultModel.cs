namespace Gateway.Application.Models.User
{
    /// <summary>
    /// ДТО между слоями GRPC сервера и GRPC Клиента
    /// </summary>
    public class LogoutResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

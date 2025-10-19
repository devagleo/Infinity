namespace Gateway.Application.Models.User
{
    /// <summary>
    /// ДТО между слоями GRPC сервера и GRPC Клиента
    /// </summary>
    public class UserModel
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}

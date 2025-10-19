using Gateway.Application.Models.User;

namespace Gateway.Application.Interfaces
{
    /// <summary>
    /// Интерфейс проксирующий доступ к Grpc клиенту
    /// </summary>
    public interface IUserGrpcClient
    {
        Task<UserModel?> GetUserByIdAsync(string userId, string jwtToken);
        Task<UserModel?> GetUserIdByLoginAsync(string login, string jwtToken);
        Task<RegisterResultModel?> RegisterAsync(string username, string password);
        Task<LoginResultModel> LoginAsync(string username, string password);
        Task<LogoutResultModel> LogoutAsync(string token);
    }
}

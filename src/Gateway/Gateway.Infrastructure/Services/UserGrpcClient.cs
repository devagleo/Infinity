using Gateway.Application.Interfaces;
using Gateway.Application.Models.User;
using Gateway.Infrastructure.Mappers.User;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using User.Grpc;

namespace Gateway.Infrastructure.Services
{
    /// <summary>
    /// GPRC клиент проксирующий запросы
    /// </summary>
    public class UserGrpcClient : IUserGrpcClient
    {
        private readonly UserProtoService.UserProtoServiceClient _client;
        private readonly ILogger<UserGrpcClient> _logger;

        public UserGrpcClient(UserProtoService.UserProtoServiceClient client, ILogger<UserGrpcClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<UserModel?> GetUserByIdAsync(string userId, string jwtToken)
        {
            try
            {
                var headers = new Metadata();
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Если токен приходит с "Bearer ", оставляем как есть
                    headers.Add("Authorization", jwtToken);
                }
                var response = await _client.GetUserByIdAsync(new GetUserByIdRequest { UserId = userId }, headers);
                return response.ToModel();
            }
            catch (Grpc.Core.RpcException ex)
            {
                _logger.LogError(ex, "Ошибвка вызова GetUserByIdAsync");
                return null;
            }
        }

        /// <summary>
        /// Получить пользователя по его логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="jwtToken">JWT Токен для последующей авторизации на другом микросервисе</param>
        /// <returns></returns>
        public async Task<UserModel?> GetUserIdByLoginAsync(string login, string jwtToken)
        {
            try
            {
                var headers = new Metadata();
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Если токен приходит с "Bearer ", оставляем как есть
                    headers.Add("Authorization", jwtToken);
                }
                var response = await _client.GetUserIdByLoginAsync(new GetUserIdByLoginRequest { Login = login }, headers);
                return response.ToModel();
            }
            catch (Grpc.Core.RpcException ex)
            {
                _logger.LogError(ex, "Ошибка вызова GetUserIdByLoginAsync");
                return null;
            }
        }

        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <param name="username">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public async Task<RegisterResultModel> RegisterAsync(string username, string password)
        {
            var response = await _client.RegisterAsync(new RegisterRequest
            {
                Username = username,
                Password = password
            });
            return response.ToModel();
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="username">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public async Task<LoginResultModel> LoginAsync(string username, string password)
        {
            var response = await _client.LoginAsync(new LoginRequest
            {
                Username = username,
                Password = password
            });
            return response.ToModel();
        }

        /// <summary>
        /// Логаут пользователя
        /// </summary>
        /// <param name="token">JWT Токен</param>
        /// <returns></returns>
        public async Task<LogoutResultModel> LogoutAsync(string token)
        {
            var result = await _client.LogoutAsync(new LogoutRequest { Token = token });
            return result.ToModel();
        }
    }
}

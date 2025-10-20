using Gateway.Application.Interfaces;
using Gateway.User.Grpc;
using Grpc.Core;

namespace Gateway.Grpc.Services
{
    /// <summary>
    /// Реализация GRPC
    /// </summary>
    public class GatewayUserGrpcService : GatewayUserProtoService.GatewayUserProtoServiceBase
    {
        private readonly IUserGrpcClient _userClient;

        public GatewayUserGrpcService(IUserGrpcClient userClient)
        {
            _userClient = userClient;
        }

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UserGrpcDTO> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var user = await _userClient.GetUserByIdAsync(request.UserId, context.RequestHeaders.GetValue("Authorization")!);
            return new UserGrpcDTO
            {
                Id = user?.Id ?? "",
                Name = user?.Username ?? ""
            };
        }


        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UserGrpcDTO> GetUserIdByLogin(GetUserIdByLoginRequest request, ServerCallContext context)
        {
            var user = await _userClient.GetUserIdByLoginAsync(request.Login, context.RequestHeaders.GetValue("Authorization")!);
            return new UserGrpcDTO
            {
                Id = user?.Id ?? "",
                Name = user?.Username ?? ""
            };
        }

        /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            var result = await _userClient.RegisterAsync(request.Username, request.Password);
            return new RegisterResponse
            {
                Id = result?.Id ?? "",
                Username = result?.Username ?? "",
                Message = result?.Message ?? ""
            };
        }

        /// <summary>
        /// Авторизовать пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var result = await _userClient.LoginAsync(request.Username, request.Password);
            return new LoginResponse
            {
                Token = result?.Token ?? "",
                Message = result?.Message ?? ""
            };
        }

        /// <summary>
        /// Логаут пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
        {
            var result = await _userClient.LogoutAsync(request.Token);
            return new LogoutResponse
            {
                Success = result?.Success ?? false,
                Message = result?.Message ?? ""
            };
        }
    }
}

using Grpc.Core;
using User.Grpc;
using User.Application.Services;
using User.Application.Interfaces;

namespace User.Grpc.Services
{
    public class UserGrpcService : UserProtoService.UserProtoServiceBase
    {
        private readonly IUserService _userService;

        public UserGrpcService(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            var user = await _userService.RegisterAsync(new Application.DTOs.RegisterRequestDTO
            {
                Username = request.Username,
                Password = request.Password
            });

            return new RegisterResponse
            {
                Id = user.Id.ToString(),
                Username = user.Name,
                Message = "����������� ������ �������"
            };
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var result = await _userService.LoginAsync(new Application.DTOs.LoginRequestDTO
            {
                Username = request.Username,
                Password = request.Password
            });

            if (result == null)
            {
                return new LoginResponse
                {
                    Message = "�� ������ ��� ������������ ��� ������"
                };
            }

            return new LoginResponse
            {
                Token = result.Token,
                Message = "����������� �������"
            };
        }

        public override Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
        {
            // Stateless JWT � ������ ���������� �����.
            return Task.FromResult(new LogoutResponse
            {
                Success = true,
                Message = "������ ������� (token invalidated client-side)"
            });
        }

        public override async Task<UserGrpcDTO> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var user = await _userService.GetByIdAsync(Guid.Parse(request.UserId));
            if (user == null)
                throw new RpcException(new Status(StatusCode.NotFound, "����������� �� ������"));
            //TODO: ������� ������
            return new UserGrpcDTO
            {
                Id = user.Id.ToString(),
                Name = user.Name
            };
        }

        public override async Task<UserGrpcDTO> GetUserIdByLogin(GetUserIdByLoginRequest request, ServerCallContext context)
        {
            var user = await _userService.GetByLoginAsync(request.Login);
            if (user == null)
                throw new RpcException(new Status(StatusCode.NotFound, "����������� �� ������"));
            //TODO: ������� ������
            return new UserGrpcDTO
            {
                Id = user.Id.ToString(),
                Name = user.Name
            };
        }
    }
}

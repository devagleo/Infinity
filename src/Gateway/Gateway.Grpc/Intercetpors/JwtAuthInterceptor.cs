using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Gateway.Grpc.Interceptors
{
    /// <summary>
    /// Интерцептор валидации JWT Токенов
    /// </summary>
    /// TODO: Сделать интерцептор общим в Shared
    public class JwtAuthInterceptor : Interceptor
    {
        private readonly JwtOptions _options;

        // Методы, которые **не требуют токена**
        private readonly HashSet<string> _whiteListMethods = new()
        {
            "/GatewayUserProtoService/Register",
            "/GatewayUserProtoService/Login",
            "/GatewayUserProtoService/Logout"
        };


        public JwtAuthInterceptor(IOptions<JwtOptions> jwtOptions)
        {
            _options = jwtOptions.Value;
        }

        /// <summary>
        /// Перехватываем запрос
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            //Проверяем нет ли запрашиваемого пути в списке исключений из авторизации
            if (!_whiteListMethods.Contains(context.Method))
            {
                ValidateJwt(context);
            }
            return await continuation(request, context);
        }

        /// <summary>
        /// Валидируем JWT токен
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="RpcException"></exception>
        private void ValidateJwt(ServerCallContext context)
        {
            // Получаем токен из метаданных
            var authHeader = context.RequestHeaders.GetValue("Authorization");
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                throw new RpcException(new Status(StatusCode.Unauthenticated, "JWT token is missing"));
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true
            }, out SecurityToken validatedToken);
        }
    }
}

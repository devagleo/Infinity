namespace Gateway.Api.Helpers
{
    /// <summary>
    /// Вспомогательный класс для быстрого изъятия из заголовка JWT Токена
    /// </summary>
    public static class JwtAcceptor
    {
        /// <summary>
        /// Получить JWT Токен из хэдера
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetJwtToken(HttpContext context)
        {
            // Получаем заголовок Authorization
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            return authHeader;
        }
    }
}

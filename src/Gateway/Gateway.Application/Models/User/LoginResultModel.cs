using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Application.Models.User
{
    /// <summary>
    /// ДТО между слоями GRPC сервера и GRPC Клиента
    /// </summary>
    public class LoginResultModel
    {
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}

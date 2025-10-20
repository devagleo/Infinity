using Gateway.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Grpc;

namespace Gateway.Infrastructure.Mappers.User
{
    /// <summary>
    /// Вспомогательный класс перевода маппинга моделей между слоями
    /// </summary>
    public static class UserGrpcMapper
    {
        public static UserModel ToModel(this UserGrpcDTO dto)
        {
            if (dto == null) return new UserModel();
            return new UserModel
            {
                Id = dto.Id,
                Username = dto.Name
            };
        }

        public static RegisterResultModel ToModel(this RegisterResponse response)
        {
            return new RegisterResultModel
            {
                Id = response.Id,
                Username = response.Username,
                Message = response.Message
            };
        }

        public static LoginResultModel ToModel(this LoginResponse response)
        {
            return new LoginResultModel
            {
                Token = response.Token,
                Message = response.Message
            };
        }

        public static LogoutResultModel ToModel(this LogoutResponse response)
        {
            return new LogoutResultModel
            {
                Success = response.Success,
                Message = response.Message
            };
        }
    }
}

using User.Domain;

namespace User.Application.DTOs
{
    /// <summary>
    /// Промежуточный DTO между Grpc и Application
    /// </summary>
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public UserDTO() { }
        
        /// <summary>
        /// Быстрая конвертация из Entity в DTO
        /// </summary>
        /// <param name="user"></param>
        public UserDTO(UserEntity user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}

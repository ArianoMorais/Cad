using UserModule.Domain.Entities;

namespace UserModule.Domain.Domain.Dtos
{
    public class UserDto : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<AdressDto> Addresses { get; set; } = new List<AdressDto>();
    }
}

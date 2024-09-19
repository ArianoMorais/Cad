

namespace UserModule.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}

using UserModule.Domain.Entities;

namespace UserModule.Domain.Domain.Dtos
{
    public class AdressDto : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}

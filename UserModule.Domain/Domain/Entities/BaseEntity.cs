

namespace UserModule.Domain.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long? UserId { get; set; }
    }
}

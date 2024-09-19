
namespace UserModule.Domain.Entities
{
    public class ChangeLog : BaseEntity
    {
        public List<ChangeDetail> Changes { get; set; } = new List<ChangeDetail>();
    }
}

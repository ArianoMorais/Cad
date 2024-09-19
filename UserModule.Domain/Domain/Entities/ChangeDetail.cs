

namespace UserModule.Domain.Entities
{
    public class ChangeDetail : BaseEntity
    {
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}

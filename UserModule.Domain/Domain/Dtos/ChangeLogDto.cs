using UserModule.Domain.Entities;

namespace UserModule.Domain.Domain.Dtos
{
    public class ChangeLogDto : BaseEntity
    {
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}

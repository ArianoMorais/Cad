using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long? UserId { get; set; }
    }
}

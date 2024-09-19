using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Domain.Entities
{
    public class ChangeLog : BaseEntity
    {
        public List<ChangeDetail> Changes { get; set; } = new List<ChangeDetail>();
    }
}

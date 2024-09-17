using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Domain.Dtos
{
    public class ChangeLogDto : BaseEntity
    {
        public string EntityId { get; set; } // ID do usuário ou da entidade alterada

        public string EntityName { get; set; } // Nome da entidade (ex: User)

        public string FieldName { get; set; } // Nome do campo alterado

        public string OldValue { get; set; } // Valor antigo

        public string NewValue { get; set; } // Valor novo
    }
}

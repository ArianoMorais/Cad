﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Domain.Entities
{
    public class ChangeDetail : BaseEntity
    {
        public string FieldName { get; set; } // Nome do campo alterado
        public string OldValue { get; set; } // Valor antigo
        public string NewValue { get; set; } // Valor novo
    }
}

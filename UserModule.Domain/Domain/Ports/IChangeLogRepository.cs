﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IChangeLogRepository : IRepository<ChangeLog>
    {

    }
}

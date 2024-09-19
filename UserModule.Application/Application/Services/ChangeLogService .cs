using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Domain.Exceptions;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;

namespace UserModule.Application.Services
{
    public class ChangeLogService : Service<ChangeLog>, IChangeLogService
    {
        private readonly IChangeLogRepository _changeLogRepository;
        public ChangeLogService(IChangeLogRepository repository, IChangeLogRepository changeLogRepository) : base(repository)
        {
            _changeLogRepository = changeLogRepository;
        }

        
    }
}

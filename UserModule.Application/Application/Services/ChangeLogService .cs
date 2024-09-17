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

        public async Task LogChangesAsync<T>(T oldEntity, T newEntity, long? userId)
        {
            var fields = typeof(T).GetProperties()
                .Where(prop => prop.CanRead)
                .Select(prop => new
                {
                    Name = prop.Name,
                    OldValue = prop.GetValue(oldEntity)?.ToString(),
                    NewValue = prop.GetValue(newEntity)?.ToString()
                })
                .Where(field => field.OldValue != field.NewValue)
                .ToList();

            var tasks = fields
                .Select(field => LogChangeAsync(userId, field.Name, field.OldValue, field.NewValue))
                .ToList();

            await Task.WhenAll(tasks);
        }
        private async Task LogChangeAsync(long? userId, string fieldName, string oldValue, string newValue)
        {
            var changeLog = new ChangeLog
            {
                UserId = userId,
                FieldName = fieldName,
                OldValue = oldValue,
                NewValue = newValue
            };

            await _changeLogRepository.SaveAsync(changeLog);
        }
    }
}

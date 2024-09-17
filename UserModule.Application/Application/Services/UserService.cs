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
    public class UserService : Service  <User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChangeLogService _changeLogService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IUserRepository userRepository, IMapper mapper) : base(repository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private async Task<List<string>> ValidateUserAsync(UserDto userDto)
        {
            var errors = new List<string>();

            if (userDto.Id != 0)
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
                if (existingUser == null)
                {
                    errors.Add("Usuário não encontrado.");
                }
            }

            if (await _userRepository.HasDuplicateAddressAsync(userDto))
            {
                errors.Add("Há um endereço duplicado.");
            }

            var userWithSameCpfOrEmail = await _userRepository.FindByCpfOrEmailAsync(userDto.Cpf, userDto.Email);
            if (userWithSameCpfOrEmail != null && userWithSameCpfOrEmail.Id != userDto.Id)
            {
                errors.Add("Um usuário com o mesmo CPF ou Email já existe.");
            }

            if (userDto.Addresses != null && userDto.Addresses.GroupBy(a => a).Any(g => g.Count() > 1))
            {
                errors.Add("O endereço não pode ser cadastrado mais de uma vez para o mesmo usuário.");
            }

            return errors;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            var validationErrors = await ValidateUserAsync(userDto);

            if (validationErrors.Any())
            {
                throw new CadException("Erro de validação", validationErrors);
            }

            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            await _userRepository.SaveAsync(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            var user = _mapper.Map<User>(userDto);

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _userRepository.UpdateAsync(user);
        }

    }
}

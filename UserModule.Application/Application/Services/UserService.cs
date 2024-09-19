using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IUserRepository userRepository, IMapper mapper, IConfiguration configuration) : base(repository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        private async Task<List<string>> ValidateUserAsync(UserDto userDto)
        {
            var errors = new List<string>();

            if (!IsValidCpf(userDto.Cpf))
                    errors.Add("O CPF fornecido é inválido.");

            if (string.IsNullOrWhiteSpace(userDto.Name) || userDto.Name.Length < 4)
                    errors.Add("O nome do usuário deve ter pelo menos 4 caracteres.");

            else if (!System.Text.RegularExpressions.Regex.IsMatch(userDto.Name, @"^[a-zA-Z\s]+$"))
                    errors.Add("O nome do usuário deve conter apenas letras e espaços.");

            if (userDto.Password != null && !IsValidPassword(userDto.Password))
                    errors.Add("A senha deve conter letras maiúsculas, minúsculas e caracteres especiais.");

            if (!IsValidEmail(userDto.Email))
                    errors.Add("O endereço de e-mail fornecido é inválido.");

            if (!IsValidPhoneNumber(userDto.PhoneNumber))
                    errors.Add("O número de telefone fornecido é inválido.");

            if (userDto.Addresses != null)
            {
                var duplicateAddresses = userDto.Addresses
                    .GroupBy(a => new { a.Street, a.City, a.State, a.ZipCode })
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateAddresses.Any())
                        errors.Add("O endereço não pode ser cadastrado mais de uma vez para o mesmo usuário.");
            }

            if (userDto.Id != null)
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
                if (existingUser == null)
                {
                    errors.Add("Usuário não encontrado.");
                }
            }

            var userWithSameCpfOrEmail = await _userRepository.FindByCpfOrEmailAsync(userDto.Cpf, userDto.Email);
            if (userWithSameCpfOrEmail != null && userWithSameCpfOrEmail.Id != userDto.Id)
                    errors.Add("Um usuário com o mesmo CPF ou Email já existe.");

            return errors;
        }

        private bool IsValidCpf(string cpf)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(cpf, @"^\d{11}$");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\(\d{2}\) \d{9}$");
        }

        public async Task<LoginDto> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            var entity = _mapper.Map<LoginDto>(user);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            entity.Token = tokenString;

            return entity;
        }

        public async Task<UserDto> GetUserDtoByIdAsync(string id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            var result = _mapper.Map<UserDto>(entity);

            return result;
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
                throw new InvalidOperationException("Usuário não encontrado.");

            var validationErrors = await ValidateUserAsync(userDto);

            if (validationErrors.Any())
                throw new CadException(validationErrors);

            var user = _mapper.Map<User>(userDto);

            if (string.IsNullOrEmpty(userDto.Password))
                    user.Password = existingUser.Password;
            else
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            await _userRepository.UpdateAsync(user);
        }
    }
}

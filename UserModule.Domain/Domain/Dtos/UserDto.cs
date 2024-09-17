using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Domain.Dtos
{
    public class UserDto : BaseEntity
    {
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "O nome deve conter apenas letras e espaços.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O endereço de e-mail é inválido.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "O número de telefone deve seguir o formato (DDD) NNNN-NNNN ou (DDD) NNNNN-NNNN.")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "A senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.")]
        public string Password { get; set; }
        public List<AdressDto> Addresses { get; set; } = new List<AdressDto>();
    }
}

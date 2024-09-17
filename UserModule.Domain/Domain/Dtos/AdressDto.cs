using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Domain.Dtos
{
    public class AdressDto : BaseEntity
    {
        [Required(ErrorMessage = "A rua é obrigatória.")]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "O endereço deve conter apenas letras e números.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve seguir o formato XXXXX-XXX.")]
        public string ZipCode { get; set; }
    }
}

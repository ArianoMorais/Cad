using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Domain.Entities
{
    public class Address : BaseEntity
    {
        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "O endereço deve conter apenas letras e números.")]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve seguir o formato XXXXX-XXX.")]
        public string ZipCode { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Street == address.Street &&
                   City == address.City &&
                   State == address.State &&
                   ZipCode == address.ZipCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, ZipCode);
        }
    }
}

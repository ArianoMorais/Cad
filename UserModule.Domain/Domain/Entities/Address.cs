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
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //public override bool Equals(object obj)
        //{
        //    return obj is Address address &&
        //           Street == address.Street &&
        //           City == address.City &&
        //           State == address.State &&
        //           ZipCode == address.ZipCode;
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Street, City, State, ZipCode);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Domain.Domain.Exceptions
{
    public class CadException : Exception
    {
        public List<string> Errors { get; }

        public CadException()
        {
            Errors = new List<string>();
        }

        public CadException(List<string> errors)
        {
            Errors = errors;
        }

        public CadException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public CadException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
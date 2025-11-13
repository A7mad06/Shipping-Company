using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class RegisterValidationException:Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public RegisterValidationException(IEnumerable<string> errors) : base("Registeration Validation Errors")
        {
            Errors = errors;
        }
    }
}

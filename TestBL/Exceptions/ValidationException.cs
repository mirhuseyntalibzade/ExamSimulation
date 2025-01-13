using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBL.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string mes) : base(mes)
        {
            
        }

        public ValidationException()
        {
            
        }
    }
}

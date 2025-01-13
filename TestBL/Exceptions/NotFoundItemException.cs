using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBL.Exceptions
{
    public class NotFoundItemException : Exception
    {
        public NotFoundItemException(string mes) : base(mes)
        {
            
        }
        public NotFoundItemException()
        {
            
        }
    }
}

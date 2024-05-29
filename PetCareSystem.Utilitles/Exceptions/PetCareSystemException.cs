using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Utilitles.Exceptions
{
    public class PetCareSystemException : Exception
    {
        public PetCareSystemException() { }
        public PetCareSystemException(string message) : base(message) { }
        public PetCareSystemException(string message, Exception inner)
            : base(message, inner) { }

    }
}

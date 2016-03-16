using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Exceptions
{
    public class DALImplementationNotFoundException : Exception
    {
        public DALImplementationNotFoundException(Type dalType)
            :base(string.Format("Implementation of {0} not found!", dalType.Name ))
        {            
        }
    }
}

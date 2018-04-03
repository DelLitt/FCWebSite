using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCDAL.Exceptions
{
    public class DalMappingException : Exception
    {
        public DalMappingException(string mapInstanceName, Type mapType)
            :base(string.Format("Cannot map {0} to {1} type!", mapInstanceName, mapType.Name ))
        {            
        }
    }
}

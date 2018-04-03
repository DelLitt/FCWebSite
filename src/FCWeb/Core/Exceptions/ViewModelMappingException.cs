namespace FCWeb.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ViewModelMappingException : Exception
    {
        public ViewModelMappingException(string mapInstanceName, Type mapType)
            :base(string.Format("View model mapping error. Cannot map {0} to {1} type!", mapInstanceName, mapType.Name ))
        {
        }
    }
}

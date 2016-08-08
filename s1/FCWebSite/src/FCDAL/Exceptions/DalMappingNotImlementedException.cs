namespace FCDAL.Exceptions
{
    using System;

    public class DalMappingNotImlementedException : Exception
    {
        public DalMappingNotImlementedException(Type type)
            :base(string.Format("Mapping is not implemented for the type {0}!", type))
        {            
        }
    }
}

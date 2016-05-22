namespace FCCore.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType)
            :base(string.Format("Entity of class {0} is not found!", entityType.Name ))
        {            
        }
    }
}

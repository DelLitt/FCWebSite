using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.DAL
{
    public interface IDalBase
    {
         void SetContext(Object context);
         Object GetContext();
    }
}

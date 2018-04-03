using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.Dal
{
    public interface IDalBase
    {
        void SetContext(object context);
        object GetContext();
    }
}

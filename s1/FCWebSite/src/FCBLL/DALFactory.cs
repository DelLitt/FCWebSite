using FCCore.Abstractions.DAL;
using FCCore.Configuration;
using FCCore.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBLL
{
    public sealed class DALFactory
    {
        public static T Create<T>(IDalBase dalBase = null) where T : IDalBase
        {
            T dal = MainCfg.ServiceProvider.GetService<T>();

            if (dal == null)
            {
                throw new DALImplementationNotFoundException(typeof(T));
            }

            if(dalBase != null)
            {
                dal.SetContext(dalBase.GetContext());
            }

            return dal;
        }
    }
}

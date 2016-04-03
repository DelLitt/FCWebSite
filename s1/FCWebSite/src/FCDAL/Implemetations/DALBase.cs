using FCCore.Abstractions.Dal;
using FCDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCDAL.Implemetations
{
    public class DalBase : IDalBase
    {
        private FCDBContext context;
        protected FCDBContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new FCDBContext();
                }

                return context;
            }
        }

        public object GetContext()
        {
            return Context;
        }

        public void SetContext(object context)
        {
            this.context = (FCDBContext)context;
        }
    }
}

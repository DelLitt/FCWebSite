using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCCore.Model;
using FCDAL.Model;
using FCCore.Abstractions.Dal;

namespace FCDAL.Implemetations
{
    public sealed class SettingsVisibilityDal : ISettingsVisibilityDal
    {
        private FCDBContext context = new FCDBContext();

        public SettingsVisibility Current
        {
            get
            {
                return context.SettingsVisibility.First();
            }
        }
    }
}

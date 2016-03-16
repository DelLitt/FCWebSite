using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.DAL
{
    public interface ISettingsVisibilityDal
    {
        SettingsVisibility Current { get; }
    }
}

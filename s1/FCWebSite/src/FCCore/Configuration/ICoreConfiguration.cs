using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Configuration
{
    public interface ICoreConfiguration
    {
        IConfigurationRoot Current { get; }
    }
}

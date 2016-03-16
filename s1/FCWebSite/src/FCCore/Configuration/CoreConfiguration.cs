using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FCCore.Configuration
{
    public class CoreConfiguration : ICoreConfiguration
    {
        public CoreConfiguration(IConfigurationRoot configurationRoot)
        {
            this.Current = configurationRoot;
        }

        public IConfigurationRoot Current { get; private set; }
    }
}

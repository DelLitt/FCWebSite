using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Configuration
{
    public static class StartupMiddleware
    {
        public static void AddCoreConfiguration(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            //serviceCollection.AddSingleton<ICoreConfiguration, CoreConfiguration>();

            MainCfg.SetServiceCollection(serviceCollection);
            MainCfg.SetCoreConfiguration(configurationRoot);
        }
    }
}

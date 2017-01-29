using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCCore.Caching;
using FCCore.Abstractions;

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

        /// <summary>
        /// Adds singleton implementation of IFCCAche.
        /// Should be added after AddCoreConfiguration(...)!
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        public static void AddFCCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IFCCache, FCMemoryCache>();
        }
    }
}

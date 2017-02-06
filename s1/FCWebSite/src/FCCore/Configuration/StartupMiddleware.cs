using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCCore.Caching;
using FCCore.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;

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

        public static void AddCoreConfiguration(this IApplicationBuilder app, IConfigurationRoot configurationRoot)
        {
            MainCfg.SetServiceProvider(app.ApplicationServices);
            MainCfg.SetCoreConfiguration(configurationRoot);
        }

        /// <summary>
        /// Adds singleton implementation of IFCCAche.
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        public static void AddFCCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMemoryCache, MemoryCache>();
            serviceCollection.AddSingleton<IFCCache, FCMemoryCache>();
        }
    }
}

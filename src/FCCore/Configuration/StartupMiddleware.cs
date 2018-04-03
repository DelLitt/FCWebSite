namespace FCCore.Configuration
{
    using Caching;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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

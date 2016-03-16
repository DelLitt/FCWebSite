using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FCCore.Model;
using FCCore.Abstractions.DAL;
using FCCore.Exceptions;

namespace FCCore.Configuration
{
    public static class MainCfg
    {
        private static IServiceCollection serviceCollection;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                return serviceCollection.BuildServiceProvider();
            }
        }

        static MainCfg()
        {
            serviceCollection = new ServiceCollection();
        }

        internal static void SetServiceCollection(IServiceCollection serviceCollection)
        {
            MainCfg.serviceCollection = serviceCollection;
        }

        internal static void SetCoreConfiguration(IConfigurationRoot configurationRoot)
        {
            CoreConfig = new CoreConfiguration(configurationRoot);
        }

        public static ICoreConfiguration CoreConfig { get; private set; }

        private static SettingsVisibility settingsVisibility;
        public static SettingsVisibility SettingsVisibility
        {
            get
            {
                if (!serviceCollection.Any())
                {
                    throw new InvalidOperationException("Service collection of main configuration hasn't been inited yet. Please call AddCoreConfiguration(...) extension method in ConfigureServices(...) method of the Startup class before using the SettingsVisibility property!");
                }

                if (settingsVisibility == null)
                {
                    settingsVisibility = ServiceProvider.GetService<ISettingsVisibilityDal>().Current;

                    if(settingsVisibility == null)
                    {
                        throw new DALImplementationNotFoundException(typeof(ISettingsVisibilityDal));
                    }
                }

                return settingsVisibility;
            }
        }
    }
}

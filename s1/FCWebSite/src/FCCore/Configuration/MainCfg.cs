using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FCCore.Model;
using FCCore.Abstractions.Dal;
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

        public static IEnumerable<string> UploadRoots
        {
            get
            {
                bool emptyResult = false;
                int i = 0;
                var roots = new List<string>();

                while(!emptyResult)
                {
                    string root = CoreConfig.Current["Settings:UploadRoots:" + i];

                    if(!string.IsNullOrWhiteSpace(root))
                    {
                        roots.Add(root);
                    }
                    else
                    {
                        emptyResult = true;
                    }

                    i++;
                }

                return roots;
            }
        }

        public static string[] AllowedImageExtensions
        {
            get
            {
                bool emptyResult = false;
                int i = 0;
                var roots = new List<string>();

                while (!emptyResult)
                {
                    string root = CoreConfig.Current["Settings:AllowedImageExtensions:" + i];

                    if (!string.IsNullOrWhiteSpace(root))
                    {
                        roots.Add(root);
                    }
                    else
                    {
                        emptyResult = true;
                    }

                    i++;
                }

                return roots.ToArray();
            }
        }

        private static ImagesCfg images;
        public static ImagesCfg Images
        {
            get
            {
                if(images == null)
                {
                    images = new ImagesCfg();
                }

                return images;
            }
        }

        public class ImagesCfg
        {
            public string Persons
            {
                get
                {
                    return CoreConfig.Current["Settings:Images:Persons"];
                }
            }
        }
    }
}

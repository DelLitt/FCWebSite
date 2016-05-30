namespace FCCore.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions.Dal;
    using Exceptions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

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

        public static int TimeShift
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:TimeShift"]);
            }
        }

        public static int MainTeamId
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainTeamId"]);
            }
        }

        public static string DefaultAuthor
        {
            get
            {
                return CoreConfig.Current["Settings:DefaultAuthor"];
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
                        roots.Add(root.ToLower());
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

            public string Teams
            {
                get
                {
                    return CoreConfig.Current["Settings:Images:Teams"];
                }
            }

            public string Store
            {
                get
                {
                    return CoreConfig.Current["Settings:Images:Store"];
                }
            }
        }
    }
}

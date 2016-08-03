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
    using Model.Refs;
    using Newtonsoft.Json;
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

        public static int ReserveTeamId
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:ReserveTeamId"]);
            }
        }

        public static int MainTableTourneyId
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainTableTourneyId"]);
            }
        }

        public static IEnumerable<int> mainTeamTourneyIds;
        public static IEnumerable<int> MainTeamTourneyIds
        {
            get
            {
                if(mainTeamTourneyIds == null)
                {
                    string data = CoreConfig.Current["Settings:MainTeamTourneyIds"];
                    JsonConvert.DeserializeObject<IEnumerable<int>>(data);
                }

                return mainTeamTourneyIds;
            }
        }

        public static IEnumerable<int> reserveTeamTourneyIds;
        public static IEnumerable<int> ReserveTeamTourneyIds
        {
            get
            {
                if (reserveTeamTourneyIds == null)
                {
                    string data = CoreConfig.Current["Settings:ReserveTeamTourneyIds"];
                    JsonConvert.DeserializeObject<IEnumerable<int>>(data);
                }

                return reserveTeamTourneyIds;
            }
        }

        public static int TeamPublicationsCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:TeamPublicationsCount"]);
            }
        }

        public static int MainPublicationsCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainPublicationsCount"]);
            }
        }

        public static int MainPublicationsRowCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainPublicationsRowCount"]);
            }
        }

        public static int MainPublicationsHotCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainPublicationsHotCount"]);
            }
        }

        public static int MainPublicationsMoreCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainPublicationsMoreCount"]);
            }
        }

        public static int MainVideosCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainVideosCount"]);
            }
        }

        public static int MainVideosRowCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainVideosRowCount"]);
            }
        }

        public static int MainVideosMoreCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainVideosMoreCount"]);
            }
        }

        public static int MainGalleriesCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainGalleriesCount"]);
            }
        }

        public static int MainGalleriesRowCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainGalleriesRowCount"]);
            }
        }

        public static int MainGalleriesMoreCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MainGalleriesMoreCount"]);
            }
        }

        public static string UrlKeyRegexPattern
        {
            get
            {
                return CoreConfig.Current["Settings:UrlKeyRegexPattern"];
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

        // View model classes

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

            public string Gallery
            {
                get
                {
                    return CoreConfig.Current["Settings:Images:Gallery"];
                }
            }

            public string Store
            {
                get
                {
                    return CoreConfig.Current["Settings:Images:Store"];
                }
            }

            public string EmptyPreview
            {
                get
                {
                    return CoreConfig.Current["Settings:Images:EmptyPreview"];
                }
            }
        }
    }
}

﻿namespace FCCore.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions.Dal;
    using Common;
    using Exceptions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Model;
    using Model.Configuration;
    using Model.Refs;
    using Newtonsoft.Json;

    public static class MainCfg
    {
        private static IServiceCollection serviceCollection;
        private static IServiceProvider serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if(serviceProvider != null)
                {
                    return serviceProvider;
                }

                return serviceCollection.BuildServiceProvider();
            }
        }

        public const string ImageVariantsKeyword = "variants";

        static MainCfg()
        {
            serviceCollection = new ServiceCollection();
        }

        internal static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            MainCfg.serviceProvider = serviceProvider;
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

        private static string logPath = "Logs";
        public static string LogPath
        {
            get { return WebHelper.ToPhysicalPath(logPath); }
        }

        public static int TimeShift
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:TimeShift"]);
            }
        }

        public static int QuickGameInfoDaysShift
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:QuickGameInfoDaysShift"]);
            }
        }

        public static int TeamGamesInfoDaysShift
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:TeamGamesInfoDaysShift"]);
            }
        }

        public static int MaxGamesInfoDaysShift
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MaxGamesInfoDaysShift"]);
            }
        }
        
        public static int MaxImagesMiddlewareCacheSeconds
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MaxImagesMiddlewareCacheSeconds"]);
            }
        }

        public static int MaxImagesMiddlewareCachedRequests
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:MaxImagesMiddlewareCachedRequests"]);
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
                    mainTeamTourneyIds = JsonConvert.DeserializeObject<IEnumerable<int>>(data);
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
                    reserveTeamTourneyIds = JsonConvert.DeserializeObject<IEnumerable<int>>(data);
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

        public static int TextPublicationsDefaultCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:TextPublicationsDefaultCount"]);
            }
        }

        public static int TextPublicationsDefaultMoreCount
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:TextPublicationsDefaultMoreCount"]);
            }
        }

        public static string UrlKeyRegexPattern
        {
            get
            {
                return CoreConfig.Current["Settings:UrlKeyRegexPattern"];
            }
        }

        private static EventId logEventId = default(EventId);
        public static EventId LogEventId
        {
            get
            {
                if(logEventId.Equals(default(EventId)))
                {
                    logEventId = new EventId(
                        Convert.ToInt32(CoreConfig.Current["Settings:LogEventId"]),
                        CoreConfig.Current["Settings:LogEventName"]);
                }

                return logEventId;
            }
        }

        public static string DefaultAuthor
        {
            get
            {
                return CoreConfig.Current["Settings:DefaultAuthor"];
            }
        }

        public static IList<string> uploadRoots;
        public static IEnumerable<string> UploadRoots
        {
            get
            {
                if (uploadRoots == null)
                {
                    bool emptyResult = false;
                    int i = 0;
                    uploadRoots = new List<string>();

                    while (!emptyResult)
                    {
                        string root = CoreConfig.Current["Settings:UploadRoots:" + i];

                        if (!string.IsNullOrWhiteSpace(root))
                        {
                            uploadRoots.Add(root.ToLower());
                        }
                        else
                        {
                            emptyResult = true;
                        }

                        i++;
                    }
                }

                return uploadRoots;
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

        private static IList<ImageSizeSetting> imageSizesAvailble;
        public static IEnumerable<ImageSizeSetting> ImageSizesAvailble
        {
            get
            {
                if (imageSizesAvailble == null)
                {
                    imageSizesAvailble = new List<ImageSizeSetting>();
                    ILogger logger = ServiceProvider.GetService<ILogger<ImageSizeSetting>>();
                    bool emptyResult = false;
                    int i = 0;

                    while (!emptyResult)
                    {
                        string key = CoreConfig.Current["Settings:ImageSizesAvailble:" + i + ":Key"];

                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            try
                            {
                                imageSizesAvailble.Add(new ImageSizeSetting()
                                {
                                    Key = key,
                                    Width = Convert.ToInt32(CoreConfig.Current["Settings:ImageSizesAvailble:" + i + ":Width"]),
                                    Height = Convert.ToInt32(CoreConfig.Current["Settings:ImageSizesAvailble:" + i + ":Height"])
                                });
                            }
                            catch (JsonException ex)
                            {
                                logger.LogError(
                                    "Couldn't deserialize a config parameter of setting ImageSizesAvailble from file 'appsettings.json'. Exception message: "
                                    + Environment.NewLine
                                    + ex.ToString());
                            }
                        }
                        else
                        {
                            emptyResult = true;
                        }

                        i++;
                    }
                }

                return imageSizesAvailble;
            }
        }

        private static bool? cacheEnabled;
        public static bool CacheEnabled
        {
            get
            {
                if(!cacheEnabled.HasValue)
                {
                    bool value = false;
                    bool.TryParse(CoreConfig.Current["Settings:CacheEnabled"], out value);
                    cacheEnabled = value;
                }

                return cacheEnabled.Value;
            }
        }

        public static int CacheDefaultSeconds
        {
            get
            {
                return Convert.ToInt32(CoreConfig.Current["Settings:CacheDefaultSeconds"]);
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

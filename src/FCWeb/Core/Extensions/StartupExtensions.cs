﻿namespace FCWeb.Core.Extensions
{
    using FCBLL.Implementations;
    using FCBLL.Implementations.Components;
    using FCBLL.Implementations.ImageGallery;
    using FCBLL.Implementations.Protocol;
    using FCCore.Abstractions;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Components;
    using FCCore.Abstractions.Bll.ImageGallery;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Abstractions.Dal;
    using FCCore.Caching;
    using FCCore.Diagnostic.Logging.Simple;
    using FCCore.Media.Image.Sizing;
    using FCDAL.Implementations;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupExtensions
    {
        public static void AddBLLServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPublicationBll, PublicationBll>();
            serviceCollection.AddTransient<ITableRecordBll, TableRecordBll>();
            serviceCollection.AddTransient<ITourneyBll, TourneyBll>();
            serviceCollection.AddTransient<ITourneyTypeBll, TourneyTypeBll>();
            serviceCollection.AddTransient<IGameBll, GameBll>();
            serviceCollection.AddTransient<IRoundBll, RoundBll>();
            serviceCollection.AddTransient<ITeamBll, TeamBll>();
            serviceCollection.AddTransient<IPersonBll, PersonBll>();
            serviceCollection.AddTransient<IVideoBll, VideoBll>();
            serviceCollection.AddTransient<IPersonStatusBll, PersonStatusBll>();
            serviceCollection.AddTransient<IPersonRoleBll, PersonRoleBll>();
            serviceCollection.AddTransient<IPersonCareerBll, PersonCareerBll>();
            serviceCollection.AddTransient<ICityBll, CityBll>();
            serviceCollection.AddTransient<IStadiumBll, StadiumBll>();
            serviceCollection.AddTransient<IProtocolRecordBll, ProtocolRecordBll>();
            serviceCollection.AddTransient<IEventBll, EventBll>();
            serviceCollection.AddTransient<IEventGroupBll, EventGroupBll>();
            serviceCollection.AddTransient<IPersonStatisticsBll, PersonStatisticsBll>();
            serviceCollection.AddTransient<IImageGalleryBll, ImageGalleryBll>();
            serviceCollection.AddTransient<ICountryBll, CountryBll>();
            serviceCollection.AddTransient<ITotalizatorBll, TotalizatorBll>();
            serviceCollection.AddTransient<ITeamTypeBll, TeamTypeBll>();
            serviceCollection.AddTransient<IGameProtocolManagerFactory, GameProtocolManagerFactory>();
            serviceCollection.AddTransient<IGalleryStorageFactory, GalleryStorageFactory>();
            serviceCollection.AddTransient<IAccumulativeLog, AccumulativeLog>();
            serviceCollection.AddTransient<IRanking, Ranking>();
            serviceCollection.AddTransient<IGameFormatManager, GameFormatManager>();            
        }

        public static void AddDALServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISettingsVisibilityDal, SettingsVisibilityDal>();
            serviceCollection.AddTransient<IPublicationDal, PublicationDal>();
            serviceCollection.AddTransient<ITableRecordDal, TableRecordDal>();
            serviceCollection.AddTransient<ITourneyDal , TourneyDal>();
            serviceCollection.AddTransient<ITourneyTypeDal, TourneyTypeDal>();
            serviceCollection.AddTransient<IGameDal, GameDal>();
            serviceCollection.AddTransient<IRoundDal, RoundDal>();
            serviceCollection.AddTransient<ITeamDal, TeamDal>();
            serviceCollection.AddTransient<IPersonDal, PersonDal>();
            serviceCollection.AddTransient<IVideoDal, VideoDal>();
            serviceCollection.AddTransient<IPersonStatusDal, PersonStatusDal>();
            serviceCollection.AddTransient<IPersonRoleDal, PersonRoleDal>();
            serviceCollection.AddTransient<IPersonCareerDal , PersonCareerDal>();
            serviceCollection.AddTransient<ICityDal, CityDal>();
            serviceCollection.AddTransient<IStadiumDal, StadiumDal>();
            serviceCollection.AddTransient<IProtocolRecordDal, ProtocolRecordDal>();
            serviceCollection.AddTransient<IEventDal, EventDal>();
            serviceCollection.AddTransient<IEventGroupDal, EventGroupDal>();
            serviceCollection.AddTransient<IPersonStatisticsDal, PersonStatisticsDal>();
            serviceCollection.AddTransient<IImageGalleryDal, ImageGalleryDal>();
            serviceCollection.AddTransient<ICountryDal, CountryDal>();
            serviceCollection.AddTransient<ITotalizatorDal, TotalizatorDal>();
            serviceCollection.AddTransient<ITeamTypeDal, TeamTypeDal>();
        }

        public static void AddFCCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IImageResizer, ImageProcessorCropImageResizer>();
            serviceCollection.AddTransient<IObjectKeyGenerator, CacheKeyGenerator>();
        }
    }
}

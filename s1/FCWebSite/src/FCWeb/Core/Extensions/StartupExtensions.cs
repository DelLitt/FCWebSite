namespace FCWeb.Core.Extensions
{
    using FCBLL.Implementations;
    using FCBLL.Implementations.Protocol;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Abstractions.Dal;
    using FCDAL.Implementations;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupExtensions
    {
        public static void AddBLLServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPublicationBll, PublicationBll>();
            serviceCollection.AddTransient<ITableRecordBll, TableRecordBll>();
            serviceCollection.AddTransient<ITourneyBll, TourneyBll>();
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
        }

        public static void AddDALServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISettingsVisibilityDal, SettingsVisibilityDal>();
            serviceCollection.AddTransient<IPublicationDal, PublicationDal>();
            serviceCollection.AddTransient<ITableRecordDal, TableRecordDal>();
            serviceCollection.AddTransient<ITourneyDal , TourneyDal>();
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
        }
    }
}

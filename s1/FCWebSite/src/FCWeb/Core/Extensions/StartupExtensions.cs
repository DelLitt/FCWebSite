namespace FCWeb.Core.Extensions
{
    using FCBLL.Implemetations;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCDAL.Implemetations;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupExtensions
    {
        public static void AddBLLServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPublicationBll, PublicationBll>();
            serviceCollection.AddTransient<ITableRecordBll, TableRecordBll>();
            serviceCollection.AddTransient<ITourneyBll, TourneyBll>();
            serviceCollection.AddTransient<IGamesBll, GamesBll>();
            serviceCollection.AddTransient<IRoundBll, RoundBll>();
            serviceCollection.AddTransient<ITeamBll, TeamBll>();
            serviceCollection.AddTransient<IPersonBll, PersonBll>();
            serviceCollection.AddTransient<IVideoBll, VideoBll>();
            serviceCollection.AddTransient<IPersonStatusBll, PersonStatusBll>();
            serviceCollection.AddTransient<IPersonRoleBll, PersonRoleBll>();
            serviceCollection.AddTransient<ICityBll, CityBll>();
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
            serviceCollection.AddTransient<ICityDal, CityDal>();
        }
    }
}

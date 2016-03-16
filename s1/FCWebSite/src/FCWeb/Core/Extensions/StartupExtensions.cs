namespace FCWeb.Core.Extensions
{
    using FCBLL.Implemetations;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.DAL;
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
            serviceCollection.AddTransient<IRoundBlll, RoundBlll>();
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
        }
    }
}

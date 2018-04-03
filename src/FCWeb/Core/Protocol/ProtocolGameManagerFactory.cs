namespace FCWeb.Core.Protocol
{
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ProtocolGameManagerFactory
    {
        public static IGameProtocolManager Create(int gameId)
        {
            var gpmFactory = MainCfg.ServiceProvider.GetService<IGameProtocolManagerFactory>();
            return gpmFactory.Create(gameId);
        }
    }
}

namespace FCBLL.Implementations.Protocol
{
    using FCCore.Abstractions.Bll.Protocol;

    public class GameProtocolManagerFactory : IGameProtocolManagerFactory
    {
        public IGameProtocolManager Create(int gameId)
        {
            return new GameProtocolManager(gameId);
        }
    }
}

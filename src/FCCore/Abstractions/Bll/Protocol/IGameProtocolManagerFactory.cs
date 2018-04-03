namespace FCCore.Abstractions.Bll.Protocol
{
    public interface IGameProtocolManagerFactory
    {
        IGameProtocolManager Create(int gameId);
    }
}

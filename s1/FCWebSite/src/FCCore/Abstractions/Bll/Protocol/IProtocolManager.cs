namespace FCCore.Abstractions.Bll.Protocol
{
    using System.Collections.Generic;
    using Model;

    public interface IGameProtocolManager
    {
        Game Game { get; }
        IEnumerable<ProtocolRecord> GetMainPlayers(int teamId);
        IEnumerable<ProtocolRecord> GetReservePlayers(int teamId);
        IEnumerable<ProtocolRecord> GetGoals(int teamId);
        IEnumerable<ProtocolRecord> GetSubstitutions(int teamId);
        IEnumerable<ProtocolRecord> GetCards(int teamId);
        IEnumerable<ProtocolRecord> GetOthers(int teamId);
    }
}

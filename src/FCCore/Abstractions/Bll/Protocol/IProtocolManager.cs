namespace FCCore.Abstractions.Bll.Protocol
{
    using System.Collections.Generic;
    using Model;

    public interface IGameProtocolManager
    {
        Game Game { get; }
        bool IsAvailable { get; }
        bool IsAvailableHome { get; }
        bool IsAvailableAway { get; }
        IEnumerable<int> PersonIds { get; }
        IEnumerable<ProtocolRecord> GetMainPlayers(int teamId);
        IEnumerable<ProtocolRecord> GetReservePlayers(int teamId);
        IEnumerable<ProtocolRecord> GetGoals(int teamId);
        IEnumerable<ProtocolRecord> GetSubstitutions(int teamId);
        IEnumerable<ProtocolRecord> GetYellows(int teamId);
        IEnumerable<ProtocolRecord> GetReds(int teamId);
        IEnumerable<ProtocolRecord> GetCards(int teamId);
        IEnumerable<ProtocolRecord> GetOthers(int teamId);
    }
}

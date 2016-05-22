namespace FCCore.Abstractions.Bll.Protocol
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IProtocolRecordBll
    {
        IEnumerable<ProtocolRecord> GetProtocolRecords(int gameId);
        int SaveProtocol(IEnumerable<ProtocolRecord> protocolRecords);
        IGameProtocolManager GetGameProtocolManager(int gameId);
    }
}

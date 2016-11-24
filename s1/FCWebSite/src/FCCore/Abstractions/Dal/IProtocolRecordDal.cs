namespace FCCore.Abstractions.Dal
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IProtocolRecordDal : IDalBase
    {
        IEnumerable<ProtocolRecord> GetProtocol(int gameId);
        IEnumerable<ProtocolRecord> GetProtocol(IEnumerable<int> gameIds);
        int SaveProtocol(int gameId, IEnumerable<ProtocolRecord> protocolRecords);
    }
}

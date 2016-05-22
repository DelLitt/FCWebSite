namespace FCCore.Abstractions.Dal
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IProtocolRecordDal : IDalBase
    {      
        IEnumerable<ProtocolRecord> GetProtocol(int gameId);
        int SaveProtocol(IEnumerable<ProtocolRecord> protocolRecords);
    }
}

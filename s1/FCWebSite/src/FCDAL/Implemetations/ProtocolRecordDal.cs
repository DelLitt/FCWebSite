namespace FCDAL.Implementations
{
    using FCCore.Abstractions.Dal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;

    public class ProtocolRecordDal : DalBase, IProtocolRecordDal
    {
        public IEnumerable<ProtocolRecord> GetProtocol(int gameId)
        {
            return Context.ProtocolRecord.Where(p => p.gameId == gameId);
        }

        public int SaveProtocol(IEnumerable<ProtocolRecord> protocolRecords)
        {
            throw new NotImplementedException();
        }
    }
}

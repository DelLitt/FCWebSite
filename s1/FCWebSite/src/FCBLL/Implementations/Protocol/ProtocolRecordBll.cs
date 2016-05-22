namespace FCBLL.Implementations.Protocol
{
    using System;
    using System.Collections.Generic;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Abstractions.Bll.Protocol;

    public class ProtocolRecordBll : IProtocolRecordBll
    {
        private IProtocolRecordDal dalProtocolRecord;
        private IProtocolRecordDal DalProtocolRecord
        {
            get
            {
                if (dalProtocolRecord == null)
                {
                    dalProtocolRecord = DALFactory.Create<IProtocolRecordDal>();
                }

                return dalProtocolRecord;
            }
        }

        public IEnumerable<ProtocolRecord> GetProtocolRecords(int gameId)
        {
            return DalProtocolRecord.GetProtocol(gameId);
        }

        public IGameProtocolManager GetGameProtocolManager(int gameId)
        {
            return new GameProtocolManager(gameId);
        }

        public int SaveProtocol(IEnumerable<ProtocolRecord> protocolRecords)
        {
            throw new NotImplementedException();
        }
    }
}

namespace FCDAL.Implementations
{
    using FCCore.Abstractions.Dal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Common;
    public class ProtocolRecordDal : DalBase, IProtocolRecordDal
    {
        public IEnumerable<ProtocolRecord> GetProtocol(int gameId)
        {
            return Context.ProtocolRecord.Where(p => p.gameId == gameId);
        }

        public int SaveProtocol(IEnumerable<ProtocolRecord> protocolRecords)
        {
            if(Guard.IsEmptyIEnumerable(protocolRecords)) { return 0; }

            IEnumerable<ProtocolRecord> dbRecords = GetProtocol(protocolRecords.First().gameId);

            if(dbRecords.Any())
            {
                IEnumerable<int> dbIds = dbRecords.Select(r => r.Id);
                IEnumerable<int> removedIds = dbIds.Except(protocolRecords.Select(pr => pr.Id));

                if (removedIds.Any())
                {
                    Context.RemoveRange(dbRecords.Where(pr => dbIds.Contains(pr.Id)));
                }
            }

            Context.AddRange(protocolRecords, Microsoft.Data.Entity.GraphBehavior.SingleObject);

            return Context.SaveChanges();
        }
    }
}

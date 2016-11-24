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

        public IEnumerable<ProtocolRecord> GetProtocol(IEnumerable<int> gameIds)
        {
            return Context.ProtocolRecord.Where(p => gameIds.Contains(p.gameId));
        }

        public int SaveProtocol(int gameId, IEnumerable<ProtocolRecord> protocolRecords)
        {
            if(protocolRecords == null) { return 0; }

            List<ProtocolRecord> saveRecords = protocolRecords.ToList();
            IEnumerable<int> saveIds = saveRecords.Select(pr => pr.Id);
            IEnumerable<ProtocolRecord> dbRecords = GetProtocol(gameId);

            if(dbRecords.Any())
            {
                IEnumerable<int> dbIds = dbRecords.Select(r => r.Id);
                IEnumerable<int> removedIds = dbIds.Except(saveIds).ToList();

                if (removedIds.Any())
                {
                    IEnumerable<ProtocolRecord> removeRecords = dbRecords.Where(pr => removedIds.Contains(pr.Id));
                    Context.RemoveRange(removeRecords);
                }

                IEnumerable<ProtocolRecord> updateRecords = dbRecords.Where(pr => saveIds.Contains(pr.Id));
                if(updateRecords.Any())
                {
                    foreach (ProtocolRecord updateRecord in updateRecords)
                    {
                        ProtocolRecord saveRecord = saveRecords.First(pr => pr.Id == updateRecord.Id);

                        updateRecord.gameId = saveRecord.gameId;
                        updateRecord.teamId = saveRecord.teamId;
                        updateRecord.personId = saveRecord.personId;
                        updateRecord.eventId = saveRecord.eventId;
                        updateRecord.Minute = saveRecord.Minute;
                        updateRecord.CustomIntValue = saveRecord.CustomIntValue;
                        updateRecord.ExtraTime = saveRecord.ExtraTime;                       
                    }

                    Context.UpdateRange(updateRecords);

                    IEnumerable<int> updateIds = updateRecords.Select(pr => pr.Id);
                    saveRecords.RemoveAll(pr => updateIds.Contains(pr.Id));
                }
            }

            Context.AddRange(saveRecords, Microsoft.Data.Entity.GraphBehavior.SingleObject);

            return Context.SaveChanges();
        }
    }
}

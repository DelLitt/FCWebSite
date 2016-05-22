namespace FCBLL.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Common;
    using FCCore.Common.Constants;
    using FCCore.Model;

    public class ProtocolRecordInfo
    {
        public ProtocolRecord ProtocolRecord { get; private set; }

        public ProtocolRecordInfo(ProtocolRecord protocolRecord)
        {
            Guard.CheckNull(protocolRecord, nameof(protocolRecord));

            ProtocolRecord = protocolRecord;
        }

        public bool IsStartMain
        {
            get
            {
                return ProtocolRecord.eventId == EventId.eInStartGame && ProtocolRecord.Minute == 0;
            }
        }

        public bool IsStartReserve
        {
            get
            {
                return ProtocolRecord.eventId == EventId.eInSubstitution && ProtocolRecord.Minute == 0;
            }
        }

        public bool IsGoal
        {
            get
            {
                return EventGroupId.egGoals.Contains(ProtocolRecord.eventId);
            }
        }
    }
}

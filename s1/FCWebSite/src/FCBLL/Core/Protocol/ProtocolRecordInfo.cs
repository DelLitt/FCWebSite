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

        public bool IsSubstitution
        {
            get
            {
                return ProtocolRecord.eventId == EventId.eInSubstitution && ProtocolRecord.Minute > 0;
            }
        }

        public bool IsGoal
        {
            get
            {
                return EventGroupId.egGoals.Contains(ProtocolRecord.eventId);
            }
        }

        public bool IsYellowCard
        {
            get
            {
                return EventGroupId.egYellows.Contains(ProtocolRecord.eventId);
            }
        }

        public bool IsRedCard
        {
            get
            {
                return EventGroupId.egReds.Contains(ProtocolRecord.eventId);
            }
        }

        public bool IsMiss
        {
            get
            {
                return EventGroupId.egMisses.Contains(ProtocolRecord.eventId);
            }
        }

        public bool IsOut
        {
            get
            {
                return EventGroupId.egOuts.Contains(ProtocolRecord.eventId);
            }
        }

        public bool IsAfterGamePenalty
        {
            get
            {
                return EventGroupId.egAfterGamePenalties.Contains(ProtocolRecord.eventId);
            }
        }
    }
}

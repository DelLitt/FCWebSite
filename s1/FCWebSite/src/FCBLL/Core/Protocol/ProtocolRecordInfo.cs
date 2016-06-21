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

            if(ProtocolRecord.personId <= 0)
            {
                ProtocolRecord.personId = null;
            }

            if (ProtocolRecord.Minute < 0)
            {
                ProtocolRecord.Minute = null;
            }

            if (ProtocolRecord.CustomIntValue <= 0)
            {
                ProtocolRecord.CustomIntValue = null;
            }
        }

        public bool IsStartMain
        {
            get
            {
                return ProtocolRecord.eventId == EventId.eInStartGame
                    && ProtocolRecord.Minute == 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0
                    && ProtocolRecord.personId > 0;
            }
        }

        public bool IsStartReserve
        {
            get
            {
                return ProtocolRecord.eventId == EventId.eInSubstitution
                    && ProtocolRecord.Minute == 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0
                    && ProtocolRecord.personId > 0;
            }
        }

        public bool IsSubstitution
        {
            get
            {
                return ProtocolRecord.eventId == EventId.eInSubstitution 
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0
                    && ProtocolRecord.personId > 0
                    && ProtocolRecord.CustomIntValue > 0;
            }
        }

        public bool IsGoal
        {
            get
            {
                // Filling minute is mandatory to calculate which goalkeeper is missed goal
                return EventGroupId.egGoals.Contains(ProtocolRecord.eventId)
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0;
            }
        }

        public bool IsYellowCard
        {
            get
            {
                return EventGroupId.egYellows.Contains(ProtocolRecord.eventId)
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0
                    && ProtocolRecord.personId > 0;
            }
        }

        public bool IsRedCard
        {
            get
            {
                return EventGroupId.egReds.Contains(ProtocolRecord.eventId)
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0
                    && ProtocolRecord.personId > 0;
            }
        }

        public bool IsMiss
        {
            get
            {
                return EventGroupId.egMisses.Contains(ProtocolRecord.eventId)
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0;
            }
        }

        public bool IsOut
        {
            get
            {
                return EventGroupId.egOuts.Contains(ProtocolRecord.eventId)
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0
                    && ProtocolRecord.personId > 0;
            }
        }

        public bool IsAfterGamePenalty
        {
            get
            {
                return EventGroupId.egAfterGamePenalties.Contains(ProtocolRecord.eventId)
                    && ProtocolRecord.Minute > 0
                    && ProtocolRecord.gameId > 0
                    && ProtocolRecord.teamId > 0;
            }
        }

        public static ProtocolRecord CreateDefaultStartMain(int gameId, int teamId)
        {
            return new ProtocolRecord()
            {
                gameId = gameId,
                teamId = teamId,
                eventId = EventId.eInStartGame,
                Minute = 0
            };
        }

        public static ProtocolRecord CreateDefaultStartReserve(int gameId, int teamId)
        {
            return new ProtocolRecord()
            {
                gameId = gameId,
                teamId = teamId,
                eventId = EventId.eInSubstitution,
                Minute = 0
            };
        }        
    }
}

namespace FCBLL.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Model;

    public class ProtocolRecordPersonInfo
    {
        private Person person;
        private ProtocolRecordInfo protocolRecordInfo;

        private ProtocolRecord protocolRecord;
        public ProtocolRecord ProtocolRecord
        {
            get
            {
                return protocolRecord;
            }
        }

        public ProtocolRecordPersonInfo(Person person, ProtocolRecord protocolRecord)
        {
            this.person = person;
            this.protocolRecord = protocolRecord;

            protocolRecordInfo = new ProtocolRecordInfo(protocolRecord);
        }

        public bool IsGoal
        {
            get { return protocolRecordInfo.IsGoal && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }

        public bool IsAssist
        {
            get { return protocolRecordInfo.IsGoal && protocolRecordInfo.ProtocolRecord.CustomIntValue == person.Id; }
        }

        public bool IsStartMain
        {
            get { return protocolRecordInfo.IsStartMain && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }

        public bool IsStartReserve
        {
            get { return protocolRecordInfo.IsStartReserve && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }

        public bool IsSubstitutionOut
        {
            get { return protocolRecordInfo.IsSubstitution && protocolRecordInfo.ProtocolRecord.CustomIntValue== person.Id; }
        }

        public bool IsSubstitutionIn
        {
            get { return protocolRecordInfo.IsSubstitution && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }

        public bool IsYellowCard
        {
            get { return protocolRecordInfo.IsYellowCard && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }

        public bool IsRedCard
        {
            get { return protocolRecordInfo.IsRedCard && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }

        public bool IsMissPenalty
        {
            get { return protocolRecordInfo.IsMissPenalty && protocolRecordInfo.ProtocolRecord.personId == person.Id; }
        }
    }
}

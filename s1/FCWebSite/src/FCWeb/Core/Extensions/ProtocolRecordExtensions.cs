namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels.Protocol;

    public static class ProtocolRecordExtensions
    {
        public static ProtocolRecordViewModel ToViewModel(this ProtocolRecord protocolRecord)
        {
            if (protocolRecord == null) { return null; }

            return new ProtocolRecordViewModel()
            {
                customIntValue = protocolRecord.CustomIntValue,
                eventId = protocolRecord.eventId,
                extraTime = protocolRecord.ExtraTime,
                gameId = protocolRecord.gameId,
                id = protocolRecord.Id,
                minute = protocolRecord.Minute,
                personId = protocolRecord.personId,
                teamId = protocolRecord.teamId
            };
        }

        public static IEnumerable<ProtocolRecordViewModel> ToViewModel(this IEnumerable<ProtocolRecord> protocolRecords)
        {
            if (Guard.IsEmptyIEnumerable(protocolRecords)) { return new ProtocolRecordViewModel[0]; }

            return protocolRecords.Select(v => v.ToViewModel()).ToList();
        }

        //public static ProtocolViewModel ToProtocolViewModel(this IEnumerable<ProtocolRecord> protocolRecords)
        //{
        //    if (Guard.IsEmptyIEnumerable(protocolRecords)) { return null; }

        //    ProtocolViewModel protocolViewModel = protocolRecords

        //    return protocolRecords.Select(v => v.ToViewModel()).ToList();
        //}
    }
}

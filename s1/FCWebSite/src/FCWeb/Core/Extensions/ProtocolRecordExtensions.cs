namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCBLL.Core.Protocol;
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

        public static ProtocolRecord ToBaseModel(this ProtocolRecordViewModel protocolViewRecord)
        {
            if (protocolViewRecord == null) { return null; }

            return new ProtocolRecord()
            {
                CustomIntValue = protocolViewRecord.customIntValue,
                eventId = protocolViewRecord.eventId,
                ExtraTime = protocolViewRecord.extraTime,
                gameId = protocolViewRecord.gameId,
                Id = protocolViewRecord.id,
                Minute = protocolViewRecord.minute,
                personId = protocolViewRecord.personId,
                teamId = protocolViewRecord.teamId
            };
        }

        public static IEnumerable<ProtocolRecord> ToBaseModel(this IEnumerable<ProtocolRecordViewModel> protocolViewRecords)
        {
            if (Guard.IsEmptyIEnumerable(protocolViewRecords)) { return new ProtocolRecord[0]; }

            return protocolViewRecords.Select(v => v.ToBaseModel()).ToList();
        }

        public static IEnumerable<ProtocolRecord> ToRecordsList(this ProtocolGameViewModel protocolViewModel)
        {
            if(protocolViewModel == null) { return new ProtocolRecord[0]; }

            var protocolRecords = new List<ProtocolRecord>();

            if(protocolViewModel.home != null)
            {
                protocolRecords.AddRange(GetProtocolTeamRecords(protocolViewModel.home));
            }

            if (protocolViewModel.away != null)
            {
                protocolRecords.AddRange(GetProtocolTeamRecords(protocolViewModel.away));
            }

            return protocolRecords;
        }

        private static IEnumerable<ProtocolRecord> GetProtocolTeamRecords(ProtocolTeamViewModel protocolTeamViewModel)
        {
            var protocolRecords = new List<ProtocolRecord>();

            if (protocolTeamViewModel.main != null)
            {
                protocolRecords.AddRange(protocolTeamViewModel.main
                    .Select(pr => new ProtocolRecordInfo(pr.ToBaseModel()))
                    .Where(pr => pr.IsStartMain)
                    .Select(pr => pr.ProtocolRecord));
            }

            if (protocolTeamViewModel.reserve != null)
            {
                protocolRecords.AddRange(protocolTeamViewModel.reserve
                    .Select(pr => new ProtocolRecordInfo(pr.ToBaseModel()))
                    .Where(pr => pr.IsStartReserve)
                    .Select(pr => pr.ProtocolRecord));
            }

            if (protocolTeamViewModel.goals != null)
            {                
                protocolRecords.AddRange(protocolTeamViewModel.goals
                    .Select(pr => new ProtocolRecordInfo(pr.ToBaseModel()))
                    .Where(pr => pr.IsGoal)
                    .Select(pr => pr.ProtocolRecord));
            }

            if (protocolTeamViewModel.subs != null)
            {
                protocolRecords.AddRange(protocolTeamViewModel.subs
                    .Select(pr => new ProtocolRecordInfo(pr.ToBaseModel()))
                    .Where(pr => pr.IsSubstitution)
                    .Select(pr => pr.ProtocolRecord));
            }

            if (protocolTeamViewModel.cards != null)
            {
                protocolRecords.AddRange(protocolTeamViewModel.cards
                    .Select(pr => new ProtocolRecordInfo(pr.ToBaseModel()))
                    .Where(pr => pr.IsYellowCard || pr.IsRedCard)
                    .Select(pr => pr.ProtocolRecord));
            }

            if (protocolTeamViewModel.others != null)
            {
                protocolRecords.AddRange(protocolTeamViewModel.others
                    .Select(pr => new ProtocolRecordInfo(pr.ToBaseModel()))
                    .Where(pr => pr.IsOut || pr.IsMiss || pr.IsAfterGamePenalty)
                    .Select(pr => pr.ProtocolRecord));
            }

            return protocolRecords;
        }
    }
}

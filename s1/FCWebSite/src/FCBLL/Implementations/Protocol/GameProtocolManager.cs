namespace FCBLL.Implementations.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Protocol;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using FCCore.Exceptions;
    using FCCore.Model;

    internal class GameProtocolManager : IGameProtocolManager
    {
        private const int PlayersMainCount = 11;
        private const int PlayersReserveCount = 7;

        private IGameBll gameBll = new GameBll();
        private IProtocolRecordBll protocolRecordBll = new ProtocolRecordBll();
        private IEnumerable<ProtocolRecordInfo> records { get; set; }

        public Game Game { get; private set; }

        public bool IsAvailable
        {
            get
            {
                return records.Any();
            }
        }

        public bool IsAvailableHome
        {
            get
            {
                return records.Any(r => r.ProtocolRecord.teamId == Game.homeId);
            }
        }

        public bool IsAvailableAway
        {
            get
            {
                return records.Any(r => r.ProtocolRecord.teamId == Game.awayId);
            }
        }

        public GameProtocolManager(int gameId)
        {
            Game = gameBll.GetGame(gameId);

            if (Game == null)
            {
                throw new EntityNotFoundException(typeof(Game));
            }

            IEnumerable<ProtocolRecord> protocolRecords = protocolRecordBll.GetProtocolRecords(Game.Id);

            records = !Guard.IsEmptyIEnumerable(protocolRecords)
                ? protocolRecords.Select(pr => new ProtocolRecordInfo(pr))
                : new ProtocolRecordInfo[0];
        }

        public IEnumerable<ProtocolRecord> GetMainPlayers(int teamId)
        {
            var mainRecords = new List<ProtocolRecord>();

            mainRecords.AddRange(records
                .Where(r => r.ProtocolRecord.teamId == teamId && r.IsStartMain)
                .Select(r => r.ProtocolRecord));

            ProtocolRecord defaultStartMain = ProtocolRecordInfo.CreateDefaultStartMain(Game.Id, teamId);

            CompleteWithEmpty(mainRecords, defaultStartMain, PlayersMainCount);

            return mainRecords;
        }

        public IEnumerable<ProtocolRecord> GetReservePlayers(int teamId)
        {
            var reserveRecords = new List<ProtocolRecord>();

            reserveRecords.AddRange(records
                .Where(r => r.ProtocolRecord.teamId == teamId && r.IsStartReserve)
                .Select(r => r.ProtocolRecord));

            ProtocolRecord defaultStartReserve = ProtocolRecordInfo.CreateDefaultStartReserve(Game.Id, teamId);

            CompleteWithEmpty(reserveRecords, defaultStartReserve, PlayersReserveCount);

            return reserveRecords;
        }

        public IEnumerable<ProtocolRecord> GetGoals(int teamId)
        {
            return records.Where(r => r.ProtocolRecord.teamId == teamId && r.IsGoal).Select(r => r.ProtocolRecord);
        }

        public IEnumerable<ProtocolRecord> GetSubstitutions(int teamId)
        {
            return records.Where(r => r.ProtocolRecord.teamId == teamId && r.IsSubstitution).Select(r => r.ProtocolRecord);
        }

        public IEnumerable<ProtocolRecord> GetCards(int teamId)
        {
            return records.Where(r => r.ProtocolRecord.teamId == teamId 
                                  && (r.IsYellowCard || r.IsRedCard)).Select(r => r.ProtocolRecord);
        }


        public IEnumerable<ProtocolRecord> GetOthers(int teamId)
        {
            return records.Where(r => r.ProtocolRecord.teamId == teamId
                                  && (r.IsOut || r.IsMiss || r.IsAfterGamePenalty)).Select(r => r.ProtocolRecord);
        }

        private void CompleteWithEmpty<T>(IList<T> items, T template, int totalCount) where T: new()
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items), "Cannot complete list referenced to null with empty items.");
            }

            int emptyCount = totalCount - items.Count;

            if(emptyCount > 0)
            {
                for(int i = 0; i < emptyCount; i++)
                {
                    items.Add(template);
                }
            }
        }
    }
}

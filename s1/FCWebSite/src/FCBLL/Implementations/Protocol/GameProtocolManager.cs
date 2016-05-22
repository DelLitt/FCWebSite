﻿namespace FCBLL.Implementations.Protocol
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

    public class GameProtocolManager : IGameProtocolManager
    {
        private const int PlayersMainCount = 11;
        private const int PlayersReserveCount = 7;

        private IGameBll gameBll = new GameBll();
        private IProtocolRecordBll protocolRecordBll = new ProtocolRecordBll();
        private IEnumerable<ProtocolRecordInfo> records { get; set; }

        public Game Game { get; private set; }

        public GameProtocolManager(int gameId)
        {
            Game = gameBll.GetGame(gameId);

            if(Game == null)
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

            mainRecords.AddRange(records.Where(r => r.ProtocolRecord.teamId == teamId && r.IsStartMain).Select(r => r.ProtocolRecord));

            CompleteWithEmpty(mainRecords, PlayersMainCount);

            return mainRecords;

        }

        public IEnumerable<ProtocolRecord> GetReservePlayers(int teamId)
        {
            var reserveRecords = new List<ProtocolRecord>();

            reserveRecords.AddRange(records.Where(r => r.ProtocolRecord.teamId == teamId && r.IsStartReserve).Select(r => r.ProtocolRecord));

            CompleteWithEmpty(reserveRecords, PlayersReserveCount);

            return reserveRecords;
        }

        public IEnumerable<ProtocolRecord> GetGoals(int teamId)
        {
            return records.Where(r => r.ProtocolRecord.teamId == teamId && r.IsGoal).Select(r => r.ProtocolRecord);
        }

        private void CompleteWithEmpty<T>(IList<T> items, int totalCount) where T: new()
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items), "Cannot complete list referenced to null with empty items.");
            }

            int emptyCount = totalCount - items.Count;

            if(emptyCount > 0)
            {
                for(int i = 0; i < totalCount; i++)
                {
                    items.Add(new T());
                }
            }
        }
    }
}

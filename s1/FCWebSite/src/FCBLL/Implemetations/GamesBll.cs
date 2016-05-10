namespace FCBLL.Implemetations
{
    using FCCore.Abstractions.Bll;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;

    public class GamesBll : IGamesBll
    {
        public bool FillTeams
        {
            get
            {
                return DalGames.FillTeams;
            }

            set
            {
                DalGames.FillTeams = value;
            }
        }

        public bool FillRounds
        {
            get
            {
                return DalGames.FillRounds;
            }

            set
            {
                DalGames.FillRounds = value;
            }
        }

        private IGameDal dalGames;
        private IGameDal DalGames
        {
            get
            {
                if (dalGames == null)
                {
                    dalGames = DALFactory.Create<IGameDal>();
                }

                return dalGames;
            }
        }

        private IRoundDal dalRounds;
        private IRoundDal DalRounds
        {
            get
            {
                if (dalRounds == null)
                {
                    dalRounds = DALFactory.Create<IRoundDal>(DalGames);
                }

                return dalRounds;
            }
        }

        public IEnumerable<Game> GetTeamActualRoundGamesOfTourneys(int teamId, IEnumerable<int> tourneyIds, DateTime date)
        {
            IEnumerable<int> roundIds = DalRounds.GetRoundIdsOfTourneys(tourneyIds, teamId);

            return GetTeamActualRoundGames(teamId, roundIds, date);
        }

        public IEnumerable<Game> GetTeamActualRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date)
        {
            if(Guard.IsEmptyIEnumerable(roundIds)) { return new Game[0]; }

            Game nearestGame = DalGames.GetTeamNearestGame(teamId, roundIds, date);

            IEnumerable<Game> roundGames = null;

            if (nearestGame != null)
            {
                roundGames = DalGames.GetRoundGames(nearestGame.roundId).OrderBy(g => g.GameDate);
            }

            return roundGames ?? new Game[0];
        }

        public IEnumerable<Game> GetRoundGames(int roundId)
        {
            return DalGames.GetRoundGames(roundId);
        }

        public Game GetGame(int id)
        {
            return DalGames.GetGame(id);
        }

        public int SaveGame(Game entity)
        {
            return DalGames.SaveGame(entity);
        }
    }
}

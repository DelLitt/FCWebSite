namespace FCBLL.Implemetations
{
    using FCCore.Abstractions.Bll;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.DAL;

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
            if(!roundIds.Any()) { return new Game[0]; }

            IEnumerable<Game> games = DalGames.GetTeamNearestRoundGames(teamId, roundIds, date);

            if(!games.Any())
            {
                games = DalGames.GetRoundsGames(roundIds)
                                .OrderBy(g => g.GameDate);

                if (games.Any())
                {
                    games = games.Where(o => o.roundId == games.First().roundId);
                }
            }

            return games;
        }

        public IEnumerable<Game> GetRoundGames(int roundId)
        {
            return DalGames.GetRoundGames(roundId);
        }
    }
}

namespace FCBLL.Implementations
{
    using FCCore.Abstractions.Bll;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;

    public class GameBll : IGameBll
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

        public bool FillTourneys
        {
            get
            {
                return DalGames.FillTourneys;
            }

            set
            {
                DalGames.FillTourneys = value;
            }
        }

        public bool FillStadiums
        {
            get
            {
                return DalGames.FillStadiums;
            }

            set
            {
                DalGames.FillStadiums = value;
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

        public Game SaveGame(Game entity)
        {
            return DalGames.SaveGame(entity);
        }

        public IEnumerable<Game> GetGamesByTourney(int tourneyId)
        {
            return DalGames.GetGamesByTourney(tourneyId);
        }

        public IEnumerable<Game> GetGamesByTourneys(IEnumerable<int> tourneyIds)
        {
            IEnumerable<int> roundIds = DalRounds.GetRoundIdsOfTourneys(tourneyIds);

            return GetGamesByRounds(roundIds);
        }

        public IEnumerable<Game> GetGamesByTourneyBetweenTeams(int tourneyId, IEnumerable<int> teamIds)
        {
            return DalGames.GetGamesByTourneyBetweenTeams(tourneyId, teamIds);
        }

        public IEnumerable<Game> GetTeamGames(int teamId, IEnumerable<int> tourneyIds, DateTime date, int daysShift)
        {
            DateTime dateStart = AddDaysSoft(date, -daysShift);
            DateTime dateEnd = AddDaysSoft(date, daysShift);

            IEnumerable<Game> games = DalGames.GetTeamGames(teamId, tourneyIds, dateStart, dateEnd)
                                              .OrderByDescending(g => g.GameDate);

            return games;
        }

        public IEnumerable<Game> GetGamesByRounds(IEnumerable<int> roundIds)
        {
            return DalGames.GetGamesByRounds(roundIds);
        }

        public IEnumerable<Game> GetTeamPrevNextGames(int teamId, IEnumerable<int> tourneyIds, DateTime date, int daysShift)
        {
            const int gamesCount = 2;
            var games = new List<Game>();

            DateTime dateStart = AddDaysSoft(date, -daysShift);
            DateTime dateEnd = AddDaysSoft(date, daysShift);

            List<Game> gamesPrevNext = DalGames.GetTeamGames(teamId, tourneyIds, dateStart, dateEnd)
                                               .OrderByDescending(g => g.GameDate)
                                               .ToList();

            Game game1;
            Game game2;

            // games in period
            if (gamesPrevNext.Any())
            {
                game1 = gamesPrevNext.Where(g => g.Played).FirstOrDefault();

                if (game1 != null)
                {
                    games.Add(game1);
                }
                else
                {
                    game1 = gamesPrevNext.Last();
                    gamesPrevNext.RemoveAt(gamesPrevNext.Count - 1);
                }

                if (gamesPrevNext.Any())
                {
                    game2 = gamesPrevNext.Where(g => !g.Played && g.GameDate > game1.GameDate).LastOrDefault();

                    if (game2 != null)
                    {
                        games.Add(game2);
                    }
                }

                if (games.Count == gamesCount)
                {
                    return games;
                }
            }
            // games after
            else
            {
                dateEnd = DateTime.MaxValue;
            }

            gamesPrevNext = DalGames.GetTeamGames(teamId, tourneyIds, dateStart, dateEnd)
                .OrderByDescending(g => g.GameDate)
                .ToList();

            if(gamesPrevNext.Any())
            {
                while(gamesPrevNext.Any() && games.Count < gamesCount)
                {
                    games.Add(gamesPrevNext.Last());
                    gamesPrevNext.RemoveAt(gamesPrevNext.Count - 1);
                }

                if (games.Count == gamesCount)
                {
                    return games;
                }
            }

            // games before
            dateStart = DateTime.MinValue;
            // no games before next was added earlier
            if (!games.Any() && games.Count < gamesCount)
            { 
                gamesPrevNext = DalGames.GetTeamGames(teamId, tourneyIds, dateStart, dateEnd)
                        .OrderByDescending(g => g.GameDate)
                        .ToList();

                if (gamesPrevNext.Any())
                {
                    while (gamesPrevNext.Any() && games.Count < gamesCount)
                    {
                        games.Insert(0, gamesPrevNext.First());
                        gamesPrevNext.RemoveAt(0);
                    }

                    if (games.Count == gamesCount)
                    {
                        return games;
                    }
                }
            }

            return games;
        }

        public int RemoveGame(int gameId, bool removeProtocol = true)
        {
            return DalGames.RemoveGame(gameId, removeProtocol);
        }

        public bool SaveGameNote(int gameId, string note)
        {
            Game game = DalGames.GetGame(gameId);

            if(game == null) { return false; }

            game.Note = note;

            return DalGames.SaveGame(game).Id > 0;
        }

        #region Private helpers
        private DateTime AddDaysSoft(DateTime date, int daysCount)
        {
            DateTime result;

            try
            {
                result = date.AddDays(daysCount);
            }
            catch (ArgumentOutOfRangeException)
            {
                result = daysCount > 0 ? DateTime.MaxValue : DateTime.MinValue;
            }

            return result;
        }
        #endregion
    }
}

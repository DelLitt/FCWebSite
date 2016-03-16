namespace FCDAL.Implemetations
{
    using FCCore.Abstractions.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCDAL.Exceptions;

    public class GameDal : DalBase, IGameDal
    {
        private const int LimitEntitiesSelections = 100;

        public bool FillTeams { get; set; } = false;

        public bool FillRounds { get; set; } = false;

        public IEnumerable<Game> GetTeamNearestRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date)
        {
            if (roundIds == null) { return new Game[0]; }

            IEnumerable<Game> games = Context.Game.Where(g => roundIds.Contains(g.roundId)
                                                            && (g.homeId == teamId || g.awayId == teamId)
                                                            && date >= g.GameDate)
                                                  .OrderByDescending(g => g.GameDate)
                                                  .Take(LimitEntitiesSelections)
                                                  .ToList();

            if(games.Any())
            {
                games = games.Where(o => o.roundId == games.First().roundId);
            }

            FillRelations(games);

            return games;
        }

        public IEnumerable<Game> GetRoundsGames(IEnumerable<int> roundIds)
        {
            if (roundIds == null) { return new Game[0]; }

            IEnumerable<Game> games = Context.Game.Where(g => roundIds.Contains(g.roundId))
                                                  .ToList();

            FillRelations(games);

            return games;
        }

        public IEnumerable<Game> GetRoundGames(int roundId)
        {
            IEnumerable<Game> games = Context.Game.Where(g => g.roundId == roundId);

            FillRelations(games);

            return games;
        }

        private void FillRelations(IEnumerable<Game> games)
        {
            if (games == null) { return; }

            IEnumerable<Team> teams = new Team[0];
            IEnumerable<Round> rounds = new Round[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.SetContext(Context);

                var teamIds = new List<int>();
                teamIds.AddRange(games.Select(g => g.homeId));
                teamIds.AddRange(games.Select(g => g.awayId));

                teams = teamDal.GetTeams(teamIds.Distinct());

                if (!teams.Any())
                {
                    throw new DalMappingException(nameof(teams), typeof(Game));
                }
            }

            if (FillRounds)
            {
                var roundDal = new RoundDal();
                roundDal.SetContext(Context);
                roundDal.FillTourneys = true;

                var roundIds = new List<int>();
                roundIds.AddRange(games.Select(g => (int)g.roundId));

                rounds = roundDal.GetRounds(roundIds.Distinct());

                if (!rounds.Any())
                {
                    throw new DalMappingException(nameof(rounds), typeof(Game));
                }
            }

            if (teams.Any() || rounds.Any())
            {
                foreach (Game game in games)
                {
                    if (FillTeams)
                    {
                        game.home = teams.FirstOrDefault(t => t.Id == game.homeId);
                        if (game.home == null)
                        {
                            throw new DalMappingException(nameof(game.home), typeof(Game));
                        }

                        game.away = teams.FirstOrDefault(t => t.Id == game.awayId);
                        if (game.away == null)
                        {
                            throw new DalMappingException(nameof(game.away), typeof(Game));
                        }
                    }

                    if(FillRounds)
                    {
                        game.round = rounds.FirstOrDefault(r => r.Id == game.roundId);
                        if (game.home == null)
                        {
                            throw new DalMappingException(nameof(game.round), typeof(Game));
                        }
                    }
                }
            }
        }
    }
}

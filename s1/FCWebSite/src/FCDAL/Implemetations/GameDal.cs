﻿namespace FCDAL.Implementations
{
    using FCCore.Abstractions.Dal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCDAL.Exceptions;
    using FCCore.Common;

    public class GameDal : DalBase, IGameDal
    {
        private const int LimitEntitiesSelections = 100;

        public bool FillTeams { get; set; } = false;

        public bool FillRounds { get; set; } = false;

        public Game GetTeamNearestGame(int teamId, IEnumerable<int> roundIds, DateTime date)
        {
            if (Guard.IsEmptyIEnumerable(roundIds)) { return null; }

            IEnumerable<Game> teamGames = Context.Game.Where(g => roundIds.Contains(g.roundId)
                                                            && (g.homeId == teamId || g.awayId == teamId)
                                                            && date >= g.GameDate)
                                                  .OrderByDescending(g => g.GameDate)
                                                  .Take(LimitEntitiesSelections)
                                                  .ToList();

            Game game = teamGames.FirstOrDefault();

            if(game != null)
            {
                FillRelations(new[] { game });
            }            

            return game;
        }

        public IEnumerable<Game> GetRoundsGames(IEnumerable<int> roundIds)
        {
            if (Guard.IsEmptyIEnumerable(roundIds)) { return new Game[0]; }

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

        public Game GetGame(int id)
        {
            Game game = Context.Game.FirstOrDefault(p => p.Id == id);

            if(game != null)
            {
                FillRelations(new[] { game });
            }

            return game;
        }

        public int SaveGame(Game entity)
        {
            if (entity.Id > 0)
            {
                Context.Game.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Game.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private void FillRelations(IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return; }

            IEnumerable<Team> teams = new Team[0];
            IEnumerable<Round> rounds = new Round[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.SetContext(Context);

                var teamIds = new List<int>();
                teamIds.AddRange(games.Select(g => g.homeId));
                teamIds.AddRange(games.Select(g => g.awayId));

                teams = teamDal.GetTeams(teamIds.Distinct()).ToList();

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

                rounds = roundDal.GetRounds(roundIds.Distinct()).ToList();

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
                        if (game.round == null)
                        {
                            throw new DalMappingException(nameof(game.round), typeof(Game));
                        }
                    }
                }
            }
        }
    }
}

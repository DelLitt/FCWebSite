namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class GameDal : DalBase, IGameDal
    {
        public bool FillTeams { get; set; } = false;
        public bool FillRounds { get; set; } = false;
        public bool FillStadiums { get; set; } = false;
        public bool FillTourneys { get; set; } = false;
        public bool FillProtocols { get; set; } = false;


        public Game GetGame(int id)
        {
            Game game = Context.Game.FirstOrDefault(p => p.Id == id);

            if (game != null)
            {
                FillRelations(new[] { game });
            }

            return game;
        }

        // TODO: Add option to select games where goals is not null

        public Game GetTeamNearestGame(int teamId, IEnumerable<int> roundIds, DateTime date)
        {
            if (Guard.IsEmptyIEnumerable(roundIds)) { return null; }

            IEnumerable<Game> teamGames = Context.Game.Where(g => roundIds.Contains(g.roundId)
                                                            && (g.homeId == teamId || g.awayId == teamId)
                                                            && date >= g.GameDate)
                                                  .OrderBy(g => g.GameDate)
                                                  .Take(LimitEntitiesSelections)
                                                  .ToList();

            Game game = teamGames.LastOrDefault();

            if(game != null)
            {
                FillRelations(new[] { game });
            }            

            return game;
        }

        public IEnumerable<Game> GetGamesByRounds(IEnumerable<int> roundIds)
        {
            if (Guard.IsEmptyIEnumerable(roundIds)) { return new Game[0]; }

            IEnumerable<Game> games = Context.Game.Where(g => roundIds.Contains(g.roundId)).ToList();

            FillRelations(games);

            return games;
        }

        public IEnumerable<Game> GetGamesByRoundsForPeriod(DateTime startDate, DateTime endDate, IEnumerable<int> roundIds)
        {
            if (Guard.IsEmptyIEnumerable(roundIds)) { return new Game[0]; }

            IEnumerable<Game> games = Context.Game
                                             .Where(g => g.GameDate >= startDate 
                                                        && g.GameDate <= endDate
                                                        && roundIds.Contains(g.roundId))
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

        public IEnumerable<Game> GetGamesByTourney(int tourneyId)
        {
            IEnumerable<Game> games = Context.Game.Where(g => tourneyId == g.round.tourneyId).ToList();

            FillRelations(games);

            return games;
        }

        public IEnumerable<Game> GetGamesByTourneys(IEnumerable<int> tourneyIds)
        {
            if(Guard.IsEmptyIEnumerable(tourneyIds)) { return new Game[0]; }

            IEnumerable<Game> games = Context.Game.Where(g => tourneyIds.Contains(g.round.tourneyId)).ToList();

            FillRelations(games);

            return games;
        }

        public IEnumerable<Game> GetGamesByTourneyBetweenTeams(int tourneyId, IEnumerable<int> teamIds)
        {
            if (Guard.IsEmptyIEnumerable(teamIds)) { return new Game[0]; }

            IEnumerable<Game> games = Context.Game.Where(g => tourneyId == g.round.tourneyId
                                                          && teamIds.Contains(g.homeId)
                                                          && teamIds.Contains(g.awayId))
                                                  .ToList();

            FillRelations(games);

            return games;
        }
        

        public IEnumerable<Game> GetTeamGames(int teamId, IEnumerable<int> tourneyIds, DateTime dateStart, DateTime dateEnd)
        {
            Guard.CheckNull(tourneyIds, nameof(tourneyIds));

            IEnumerable<short> roundIds = Context.Round.Where(r => tourneyIds.Contains(r.tourneyId))
                                                       .Select(r => r.Id)
                                                       .ToList();

            if(!roundIds.Any()) { return new Game[] { }; }

            IEnumerable<Game> gamesOfRounnds = Context.Game.Where(g => roundIds.Contains(g.roundId))
                                                          .ToList();

            if(!gamesOfRounnds.Any())
            {
                return new Game[] { };
            }

            IEnumerable<Game> games = gamesOfRounnds.Where(g => g.GameDate >= dateStart && g.GameDate <= dateEnd
                                                           && (g.homeId == teamId || g.awayId == teamId));

            FillRelations(games);

            return games;
        }

        public IEnumerable<Game> GetTourneyGames(int tourneyId, IEnumerable<int> teamIds, DateTime dateStart, DateTime dateEnd)
        {
            IQueryable<Game> gamesQuery = Context.Game.Where(g => g.round.tourneyId == tourneyId
                                                        && g.GameDate >= dateStart 
                                                        && g.GameDate <= dateEnd);

            if(!Guard.IsEmptyIEnumerable(teamIds))
            {
                gamesQuery = gamesQuery.Where(g => teamIds.Contains(g.homeId) || teamIds.Contains(g.awayId));
            }

            IEnumerable<Game> games = ApplySettings(gamesQuery);

            return games;
        }

        public int RemoveGame(int gameId, bool removeProtocol = true)
        {
            try
            {
                Game game = null;
                EntityEntry entry = null;

                if (removeProtocol)
                {
                    game = Context.Game
                                    .Where(g => g.Id == gameId)
                                    .Include(g => g.ProtocolRecord)
                                    .FirstOrDefault();

                    if (game == null) { return 0; }

                    entry = Context.Entry(game);
                }
                else
                {
                    game = new Game() { Id = gameId };
                    entry = Context.Attach(game);
                }

                Context.Game.Remove(game);
                Context.SaveChanges();

                return entry.State == EntityState.Detached ? gameId : 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // TODO: Add logging
                return 0;
            }
            catch
            {
                // TODO: Add logging
                throw;
            }
        }

        public Game SaveGame(Game entity)
        {
            if (entity.Id > 0)
            {
                Context.Game.Update(entity);
            }
            else
            {
                Context.Game.Add(entity);
            }

            try
            {
                Context.SaveChanges();
            }
            catch(Exception ex)
            {
                // TODO: Add logging here
                throw;
            }

            return entity;
        }

        private IEnumerable<Game> ApplySettings(IQueryable<Game> gamesQuery)
        {
            if (FillRounds)
            {
                gamesQuery = gamesQuery.Include(g => g.round);

                if (FillTourneys)
                {
                    gamesQuery = gamesQuery.Include(g => g.round.tourney);
                }
            }

            if (FillTeams)
            {
                gamesQuery = gamesQuery.Include(g => g.home).Include(g => g.away);
            }

            if (FillStadiums)
            {
                gamesQuery = gamesQuery.Include(g => g.stadium);
            }

            // TODO: Check for one to multiple
            if (FillProtocols)
            {
                gamesQuery = gamesQuery.Include(t => t.ProtocolRecord);
            }

            IEnumerable<ProtocolRecord> protocols = new ProtocolRecord[0];


            gamesQuery = gamesQuery.Take(LimitEntitiesSelections);

            IEnumerable<Game> games = gamesQuery.ToList();

            //if (FillProtocols)
            //{
            //    var protocolRecordDal = new ProtocolRecordDal();

            //    var gameIds = new List<int>();
            //    gameIds.AddRange(games.Select(g => g.Id).Distinct());

            //    protocols = protocolRecordDal.GetProtocol(gameIds).ToList();

            //    foreach (Game game in games)
            //    {
            //        if (FillProtocols && protocols.Any())
            //        {
            //            game.ProtocolRecord = protocols.Where(p => p.gameId == game.Id).ToList();
            //        }
            //        else
            //        {
            //            game.ProtocolRecord = null;
            //        }
            //    }
            //}

            return games;
        }

        [Obsolete("Should be removed. Use 'ApplySettings' method if it possible instead.")]
        private void FillRelations(IEnumerable<Game> games)
        {
            if(Guard.IsEmptyIEnumerable(games)) { return; }

            IEnumerable<Team> teams = new Team[0];
            IEnumerable<Round> rounds = new Round[0];
            IEnumerable<Stadium> stadiums = new Stadium[0];
            IEnumerable<ProtocolRecord> protocols = new ProtocolRecord[0];

            if (FillTourneys)
            {
                FillRounds = true;
            }

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
                roundDal.FillTourneys = FillTourneys;

                var roundIds = new List<int>();
                roundIds.AddRange(games.Select(g => (int)g.roundId));

                rounds = roundDal.GetRounds(roundIds.Distinct()).ToList();

                if (!rounds.Any())
                {
                    throw new DalMappingException(nameof(rounds), typeof(Game));
                }
            }

            if (FillStadiums)
            {
                var stadiumDal = new StadiumDal();
                stadiumDal.SetContext(Context);
                stadiumDal.FillCities = true;

                var stadiumIds = new List<int>();
                stadiumIds.AddRange(games.Where(s => s.stadiumId.HasValue).Select(g => (int)g.stadiumId));

                stadiums = stadiumDal.GetStadiums(stadiumIds.Distinct()).ToList();
            }

            if (FillProtocols)
            {
                var protocolRecordDal = new ProtocolRecordDal();                

                var gameIds = new List<int>();
                gameIds.AddRange(games.Select(g => g.Id).Distinct());

                protocols = protocolRecordDal.GetProtocol(gameIds).ToList();
            }

            foreach (Game game in games)
            {
                if (FillTeams && teams.Any())
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
                else
                {
                    game.home = null;
                    game.away = null;
                }

                if(FillRounds && rounds.Any())
                {
                    game.round = rounds.FirstOrDefault(r => r.Id == game.roundId);
                    if (game.round == null)
                    {
                        throw new DalMappingException(nameof(game.round), typeof(Game));
                    }
                }
                else
                {
                    game.round = null;
                }

                if (FillStadiums && stadiums.Any())
                {
                    game.stadium = stadiums.FirstOrDefault(r => r.Id == game.stadiumId);
                }
                else
                {
                    game.stadium = null;
                }

                if (FillProtocols && protocols.Any())
                {
                    game.ProtocolRecord = protocols.Where(p => p.gameId == game.Id).ToList();
                }
                else
                {
                    game.ProtocolRecord = null;
                }
            }
        }
    }
}

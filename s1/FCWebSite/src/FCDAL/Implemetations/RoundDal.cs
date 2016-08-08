namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;

    public class RoundDal : DalBase, IRoundDal
    {
        public bool FillTourneys { get; set; } = false;
        public bool FillGames { get; set; } = false;

        public Round GetRound(int id)
        {
            Round round = Context.Round.FirstOrDefault(p => p.Id == id);

            FillRelations(new Round[] { round });

            return round;
        }

        public IEnumerable<Round> GetRounds(IEnumerable<int> ids)
        {
            if (Guard.IsEmptyIEnumerable(ids)) { return new Round[0]; }

            IEnumerable<Round> rounds = Context.Round.Where(r => ids.Contains(r.Id)).ToList();

            FillRelations(rounds);

            return rounds;
        }


        public IEnumerable<Round> GetRoundsByTourney(int tourneyId)
        {
            IEnumerable<Round> rounds = Context.Round.Where(r => r.tourneyId == tourneyId).ToList();

            FillRelations(rounds);

            return rounds;
        }

        public IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null)
        {
            if(Guard.IsEmptyIEnumerable(tourneyIds)) { return new int[0]; }

            IEnumerable<int> roundIds;

            if (sortByTeamId.HasValue)
            {
                roundIds = Context.Round.Where(r => tourneyIds.Contains(r.tourneyId))
                                        .Select(r => Convert.ToInt32(r.Id))
                                        .ToList();

                if (!roundIds.Any()) { return new int[0]; }

                IEnumerable<Game> gamesByTeam = Context.Game.Where(g => roundIds.Contains(g.roundId)
                                                  && (g.homeId == sortByTeamId.Value || g.awayId == sortByTeamId.Value))
                                                .OrderBy(g => g.GameDate)
                                                .ToList();

                if (!gamesByTeam.Any()) { return new int[0]; }

                roundIds = gamesByTeam.Select(g => Convert.ToInt32(g.roundId)).Distinct();
            }
            else
            {
                roundIds = Context.Round.Where(r => tourneyIds.Contains(r.tourneyId))
                                        .Select(r => Convert.ToInt32(r.Id))
                                        .Distinct()
                                        .ToList();
            }

            return roundIds;
        }

        public IEnumerable<Round> GetRoundsOfTourneys(IEnumerable<int> tourneyIds)
        {
            if (Guard.IsEmptyIEnumerable(tourneyIds)) { return new Round[0]; }

            return Context.Round.Where(r => tourneyIds.Contains(r.tourneyId)).ToList();
        }

        public IEnumerable<Round> SearchByNameFull(int tourneyId, string text)
        {
            return Context.Round.Where(v => v.tourneyId == tourneyId && v.NameFull.Contains(text));
        }

        public Round SaveRound(Round entity)
        {
            if (entity.Id > 0)
            {
                Context.Round.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Round.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity;
        }

        private void FillRelations(IEnumerable<Round> rounds)
        {
            if (Guard.IsEmptyIEnumerable(rounds)) { return; }

            IEnumerable<Tourney> tourneys = new Tourney[0];
            GameDal dalGames = null;

            if (FillTourneys)
            {
                var tourneyDal = new TourneyDal();
                tourneyDal.SetContext(Context);

                var tourneyIds = new List<int>();
                tourneyIds.AddRange(rounds.Select(r => (int)r.tourneyId));

                tourneys = tourneyDal.GetTourneys(tourneyIds.Distinct()).ToList();

                if (!tourneys.Any())
                {
                    throw new DalMappingException(nameof(tourneys), typeof(Round));
                }
            }

            if(FillGames)
            {
                dalGames = new GameDal();
                dalGames.SetContext(Context);
            }

            if (tourneys.Any() || dalGames != null)
            {
                foreach (Round round in rounds)
                {
                    if (FillTourneys && tourneys.Any())
                    {
                        round.tourney = tourneys.FirstOrDefault(t => t.Id == round.tourneyId);

                        if (round.tourney == null)
                        {
                            throw new DalMappingException(nameof(round.tourney), typeof(Round));
                        }
                    }

                    if (dalGames != null)
                    {
                        round.Game = dalGames.GetRoundGames(round.Id).ToArray();
                    }
                }
            }
        }
    }
}

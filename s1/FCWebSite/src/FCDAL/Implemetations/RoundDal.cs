namespace FCDAL.Implemetations
{
    using System.Linq;
    using System.Collections.Generic;
    using FCCore.Model;
    using Exceptions;
    using System;
    using FCCore.Abstractions.Dal;
    public class RoundDal : DalBase, IRoundDal
    {
        public bool FillTourneys { get; set; } = false;

        public IEnumerable<Round> GetRounds(IEnumerable<int> ids)
        {
            if (ids == null) { return new Round[0]; }

            IEnumerable<Round> rounds = Context.Round.Where(r => ids.Contains(r.Id));

            FillRelations(rounds);

            return rounds;
        }

        public IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null)
        {
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
                                        .Select(r => (int)r.Id)
                                        .Distinct();
            }

            return roundIds;
        }

        private void FillRelations(IEnumerable<Round> rounds)
        {
            if (rounds == null) { return; }

            IEnumerable<Tourney> tourneys = new Tourney[0];

            if(FillTourneys)
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

            if (tourneys.Any())
            {
                foreach (Round round in rounds)
                {
                    if (FillTourneys)
                    {
                        round.tourney = tourneys.FirstOrDefault(t => t.Id == round.tourneyId);

                        if (round.tourney == null)
                        {
                            throw new DalMappingException(nameof(round.tourney), typeof(Round));
                        }
                    }
                }
            }
        }
    }
}

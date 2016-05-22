namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class TourneyDal : DalBase, ITourneyDal
    {
        public Tourney GetTourney(int tourneyId)
        {
            return Context.Tourney.FirstOrDefault(t => t.Id == tourneyId);
        }

        public Tourney GetTourneyByRoundId(int roundId)
        {
            return Context.Tourney.FirstOrDefault(t => t.Round.Any(r => r.Id == roundId));
        }

        public IEnumerable<Tourney> GetAll()
        {
            return Context.Tourney;
        }

        public IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids)
        {
            if (ids == null) { return new Tourney[0]; }

            return Context.Tourney.Where(r => ids.Contains(r.Id));
        }

        public IEnumerable<Tourney> SearchByNameFull(string text)
        {
            return Context.Tourney.Where(v => v.NameFull.Contains(text));
        }
    }
}

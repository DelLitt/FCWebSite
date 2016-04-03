namespace FCDAL.Implemetations
{
    using System.Linq;
    using FCCore.Model;
    using System.Collections.Generic;
    using FCCore.Abstractions.Dal;
    public class TourneyDal : DalBase, ITourneyDal
    {
        public Tourney GetTourney(int tourneyId)
        {
            return Context.Tourney.FirstOrDefault(t => t.Id == tourneyId);
        }

        public IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids)
        {
            if (ids == null) { return new Tourney[0]; }

            return Context.Tourney.Where(r => ids.Contains(r.Id));
        }
    }
}

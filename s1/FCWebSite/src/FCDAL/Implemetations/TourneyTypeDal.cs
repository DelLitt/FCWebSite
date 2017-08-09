namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class TourneyTypeDal : DalBase, ITourneyTypeDal
    {
        public TourneyType GetTourneyType(int tourneyTypeId)
        {
            TourneyType tourney = Context.TourneyType.FirstOrDefault(t => t.Id == tourneyTypeId);
            return tourney;
        }

        public IEnumerable<TourneyType> GetAll()
        {
            return Context.TourneyType.ToList();
        }

        public IEnumerable<TourneyType> SearchByDefault(string text)
        {
            return Context.TourneyType.Where(v => v.NameFull.Contains(text));
        }
    }
}

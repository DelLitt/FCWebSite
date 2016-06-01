namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public sealed class TourneyBll : ITourneyBll
    {
        private ITourneyDal dalTourney;
        private ITourneyDal DalTourney
        {
            get
            {
                if (dalTourney == null)
                {
                    dalTourney = DALFactory.Create<ITourneyDal>();
                }

                return dalTourney;
            }
        }

        public Tourney GetTourney(int tourneyId)
        {
            return DalTourney.GetTourney(tourneyId);
        }

        public Tourney GetTourneyByRoundId(int roundId)
        {
            return DalTourney.GetTourneyByRoundId(roundId);
        }

        public IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids)
        {
            return DalTourney.GetTourneys(ids);
        }

        public IEnumerable<Tourney> GetAll()
        {
            return DalTourney.GetAll();
        }

        public IEnumerable<Tourney> SearchByNameFull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Tourney[0]; }

            return DalTourney.SearchByNameFull(text);
        }
    }
}

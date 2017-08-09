namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public sealed class TourneyBll : FCBllBase, ITourneyBll
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

        public bool FillRounds
        {
            get
            {
                return DalTourney.FillRounds;
            }

            set
            {
                DalTourney.FillRounds = value;
            }
        }

        public bool FillGames
        {
            get
            {
                return DalTourney.FillGames;
            }

            set
            {
                DalTourney.FillGames = value;
            }
        }

        public System.DateTime Test(string key, DateTime value, int seconds)
        {
            DateTime dt = Cache.GetOrCreate(key, () => { return value; }, seconds);

            return dt;
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
            string cacheKey = GetStringMethodKey(nameof(GetTourneys), ids);

            IEnumerable<Tourney> result = Cache.GetOrCreate(cacheKey, () => { return DalTourney.GetTourneys(ids); });

            return result;
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

        public Tourney SaveTourney(Tourney entity)
        {
            return DalTourney.SaveTourney(entity);
        }
    }
}

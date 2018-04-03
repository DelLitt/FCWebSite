namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using Core.Protocol;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonStatisticsBll : FCBllBase, IPersonStatisticsBll
    {
        public bool FillTeams
        {
            get
            {
                return DalPersonStatistics.FillTeams;
            }

            set
            {
                DalPersonStatistics.FillTeams = value;
            }
        }

        public bool FillTourneys
        {
            get
            {
                return DalPersonStatistics.FillTourneys;
            }

            set
            {
                DalPersonStatistics.FillTourneys = value;
            }
        }

        private IPersonStatisticsDal dalPersonStatistics;
        private IPersonStatisticsDal DalPersonStatistics
        {
            get
            {
                if (dalPersonStatistics == null)
                {
                    dalPersonStatistics = DALFactory.Create<IPersonStatisticsDal>();
                }

                return dalPersonStatistics;
            }
        }

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

        public IEnumerable<PersonStatistics> GetPersonStatistics(int personId)
        {
            return DalPersonStatistics.GetPersonStatistics(personId);
        }

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int teamId, int tourneyId)
        {
            string cacheKey = GetStringMethodKey(nameof(GetPersonsStatistics), teamId, tourneyId);

            IEnumerable<PersonStatistics> result = Cache.GetOrCreate(cacheKey, () => { return DalPersonStatistics.GetPersonsStatistics(teamId, tourneyId); });

            return result;
        }

        public int SavePersonStatistics(int tourneyId, IEnumerable<PersonStatistics> personStatistics)
        {
            return DalPersonStatistics.SavePersonStatistics(tourneyId, personStatistics);
        }

        public IEnumerable<PersonStatistics> CalculateTourneyStatistics(int tourneyId, IEnumerable<Person> persons)
        {
            DalTourney.FillProtocols = true;

            Tourney tourney = DalTourney.GetTourney(tourneyId);

            var statsCalculator = new PersonTourneyStatsCalculator(tourney, persons);

            return statsCalculator.PersonStatistics;
        }
    }
}

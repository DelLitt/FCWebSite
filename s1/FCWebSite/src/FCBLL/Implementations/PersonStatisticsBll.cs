namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonStatisticsBll : IPersonStatisticsBll
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

        public IEnumerable<PersonStatistics> GetPersonStatistics(int personId)
        {
            return DalPersonStatistics.GetPersonStatistics(personId);
        }

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId)
        {
            return DalPersonStatistics.GetPersonsStatistics(temaId, tourneyId);
        }

        public int SavePersonStatistics(PersonStatistics entity)
        {
            return DalPersonStatistics.SavePersonStatistics(entity);
        }
    }
}

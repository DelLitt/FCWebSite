namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonStatisticsBll : IPersonStatisticsBll
    {
        private IPersonStatisticsDal personStatisticsDal;
        private IPersonStatisticsDal PersonStatisticsDal
        {
            get
            {
                if (personStatisticsDal == null)
                {
                    personStatisticsDal = DALFactory.Create<IPersonStatisticsDal>();
                }

                return personStatisticsDal;
            }
        }

        public PersonStatistics GetPersonStatistics(int personId)
        {
            return PersonStatisticsDal.GetPersonStatistics(personId);
        }

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId)
        {
            return PersonStatisticsDal.GetPersonsStatistics(temaId, tourneyId);
        }

        public int SavePersonStatistics(PersonStatistics entity)
        {
            return PersonStatisticsDal.SavePersonStatistics(entity);
        }
    }
}

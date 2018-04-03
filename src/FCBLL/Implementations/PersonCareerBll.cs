namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonCareerBll : IPersonCareerBll
    {
        public bool FillTeams
        {
            get
            {
                return DALPersonCareer.FillTeams;
            }

            set
            {
                DALPersonCareer.FillTeams = value;
            }
        }

        private IPersonCareerDal dalPersonCareer;
        private IPersonCareerDal DALPersonCareer
        {
            get
            {
                if (dalPersonCareer == null)
                {
                    dalPersonCareer = DALFactory.Create<IPersonCareerDal>();
                }

                return dalPersonCareer;
            }
        }

        public IEnumerable<PersonCareer> GetPersonCareer(int personId)
        {
            return DALPersonCareer.GetPersonCareer(personId);
        }

        public IEnumerable<int> SavePersonCareer(IEnumerable<PersonCareer> entities)
        {
            return DALPersonCareer.SavePersonCareer(entities);
        }
    }
}

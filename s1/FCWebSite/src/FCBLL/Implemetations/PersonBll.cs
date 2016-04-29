namespace FCBLL.Implemetations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonBll : IPersonBll
    {
        private IPersonDal dalPerson;
        private IPersonDal DALPerson
        {
            get
            {
                if (dalPerson == null)
                {
                    dalPerson = DALFactory.Create<IPersonDal>();
                }

                return dalPerson;
            }
        }

        public Person GetPerson(int id)
        {
            return DALPerson.GetPerson(id);
        }

        public IEnumerable<Person> GetTeamPersons(int teamId)
        {
            return DALPerson.GetTeamPersons(teamId);
        }

        public int SavePerson(Person entity)
        {
            return DALPerson.SavePerson(entity);
        }
    }
}

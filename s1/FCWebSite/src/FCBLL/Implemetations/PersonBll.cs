using FCCore.Abstractions.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCCore.Model;
using FCCore.Abstractions.Dal;

namespace FCBLL.Implemetations
{
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
    }
}

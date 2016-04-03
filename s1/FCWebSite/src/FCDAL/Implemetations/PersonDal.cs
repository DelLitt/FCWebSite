namespace FCDAL.Implemetations
{
    using FCCore.Abstractions.Dal;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;

    public class PersonDal : DalBase, IPersonDal
    {
        public Person GetPerson(int id)
        {
            return Context.Person.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Person> GetTeamPersons(int teamId)
        {
            return Context.Person.Where(p => p.teamId == teamId);
        }
    }
}

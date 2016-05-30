namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
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

        public IEnumerable<Person> GetTeamPersons(int teamId, IEnumerable<int> personRoleIds)
        {
            if (Guard.IsEmptyIEnumerable(personRoleIds)) { return new Person[0]; }

            return Context.Person.Where(p => p.teamId == teamId && personRoleIds.Contains((int)p.roleId));
        }

        public IEnumerable<Person> GetTeamPersons(int teamId, DateTime date)
        {
            return Context.Person.Where(p => p.PersonCareer.Any(
                pc => pc.teamId == teamId && pc.DateStart <= date && pc.DateFinish >= date));
        }

        public int SavePerson(Person entity)
        {
            if (entity.Id > 0)
            {
                Context.Person.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Person.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}

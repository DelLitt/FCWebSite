namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using System;

    public class PersonRoleDal : DalBase, IPersonRoleDal
    {
        public PersonRole GetPersonRole(int id)
        {
            return Context.PersonRole.FirstOrDefault(pr => pr.Id == id);
        }

        public IEnumerable<PersonRole> GetPersonRoles(IEnumerable<int> ids)
        {
            if (ids == null) { return new PersonRole[0]; }

            return Context.PersonRole.Where(pr => ids.Contains(pr.Id));
        }

        public IEnumerable<PersonRole> GetAll()
        {
            return Context.PersonRole;
        }

        public IEnumerable<PersonRole> SearchByNameFull(string text)
        {
            return Context.PersonRole.Where(pr => pr.NameFull.Contains(text));
        }
    }
}

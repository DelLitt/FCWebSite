namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public class PersonRoleDal : DalBase, IPersonRoleDal
    {
        public PersonRole GetPersonRole(int id)
        {
            return Context.PersonRole.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PersonRole> GetAll()
        {
            return Context.PersonRole;
        }

        public IEnumerable<PersonRole> SearchByNameFull(string text)
        {
            return Context.PersonRole.Where(v => v.NameFull.Contains(text));
        }
    }
}

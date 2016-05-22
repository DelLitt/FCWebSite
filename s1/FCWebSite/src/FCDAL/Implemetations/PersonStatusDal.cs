namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public class PersonStatusDal : DalBase, IPersonStatusDal
    {
        public PersonStatus GetPersonStatus(int id)
        {
            return Context.PersonStatus.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PersonStatus> GetAll()
        {
            return Context.PersonStatus;
        }

        IEnumerable<PersonStatus> IPersonStatusDal.SearchByNameFull(string text)
        {
            return Context.PersonStatus.Where(v => v.NameFull.Contains(text));
        }
    }
}

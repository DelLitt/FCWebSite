namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;

    public interface IPersonDal : IDalBase
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        int SavePerson(Person entity);
    }
}

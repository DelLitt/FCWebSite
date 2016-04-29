namespace FCCore.Abstractions.Bll
{
    using FCCore.Model;
    using System.Collections.Generic;

    public interface IPersonBll
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        int SavePerson(Person entity);
    }
}

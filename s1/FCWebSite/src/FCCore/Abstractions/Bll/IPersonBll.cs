namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;
    using System;

    public interface IPersonBll
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        IEnumerable<Person> GetTeamPersons(int teamId, DateTime date);
        int SavePerson(Person entity);
    }
}

namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;
    using System;
    using Common;
    public interface IPersonBll
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        IEnumerable<Person> GetTeamPersons(int teamId, PersonGroup personGroup);
        IEnumerable<Person> GetTeamPersons(int teamId, DateTime date);
        int SavePerson(Person entity);
    }
}

namespace FCCore.Abstractions.Dal
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface IPersonDal : IDalBase
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        IEnumerable<Person> GetTeamPersons(int teamId, IEnumerable<int> personRoleIds);
        IEnumerable<Person> GetTeamPersons(int teamId, DateTime date);
        int SavePerson(Person entity);
    }
}

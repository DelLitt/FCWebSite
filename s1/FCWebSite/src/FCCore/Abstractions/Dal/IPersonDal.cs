namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;
    using System;
    public interface IPersonDal : IDalBase
    {
        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        IEnumerable<Person> GetTeamPersons(int teamId, DateTime date);
        int SavePerson(Person entity);
    }
}

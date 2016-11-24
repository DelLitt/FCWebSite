namespace FCCore.Abstractions.Dal
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface IPersonDal : IDalBase
    {
        bool FillTeams { get; set; }
        bool FillCities { get; set; }
        bool FillPersonRoles { get; set; }
        bool FillPersonCareer { get; set; }

        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        IEnumerable<Person> GetTeamPersons(int teamId, IEnumerable<int> personRoleIds);
        IEnumerable<Person> GetTeamPersons(int teamId, DateTime date);
        IEnumerable<Person> GetPersons(IEnumerable<int> personsIds);
        IEnumerable<Person> GetPersons();
        IEnumerable<Person> SearchByDefault(string text);
        int SavePerson(Person entity);
    }
}

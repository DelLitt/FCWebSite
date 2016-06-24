﻿namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;
    using System;
    using Common;
    public interface IPersonBll
    {
        bool FillTeams { get; set; }
        bool FillCities { get; set; }
        bool FillPersonRoles { get; set; }

        Person GetPerson(int id);
        IEnumerable<Person> GetTeamPersons(int teamId);
        IEnumerable<Person> GetTeamPersons(int teamId, PersonGroup personGroup);
        IEnumerable<Person> GetTeamPersons(int teamId, DateTime date);
        IEnumerable<Person> GetPersons();
        IEnumerable<Person> GetPersons(IEnumerable<int> personsIds);
        int SavePerson(Person entity);
    }
}

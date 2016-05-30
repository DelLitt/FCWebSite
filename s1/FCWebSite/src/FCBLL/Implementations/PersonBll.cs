﻿namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Common.Constants;
    using FCCore.Model;

    public class PersonBll : IPersonBll
    {
        private IPersonDal dalPerson;
        private IPersonDal DalPerson
        {
            get
            {
                if (dalPerson == null)
                {
                    dalPerson = DALFactory.Create<IPersonDal>();
                }

                return dalPerson;
            }
        }

        public Person GetPerson(int id)
        {
            return DalPerson.GetPerson(id);
        }

        public IEnumerable<Person> GetTeamPersons(int teamId)
        {
            return DalPerson.GetTeamPersons(teamId);
        }

        public IEnumerable<Person> GetTeamPersons(int teamId, PersonGroup personGroup)
        {
            IEnumerable<int> personRoleIds = null;

            switch(personGroup)
            {
                case PersonGroup.Coaches:
                    personRoleIds = new List<int>(PersonRoleGroupId.rgCoachingStaff)
                                    .Union(new[] { PersonRoleId.rrVideographer, PersonRoleId.rrClubAdministrator });
                    break;

                case PersonGroup.Direction:
                    personRoleIds = new List<int>(PersonRoleGroupId.rgSeniorStaff)
                                    .Union(new[] { PersonRoleId.rrAccountantChief });
                    break;

                case PersonGroup.MainTeam:
                    personRoleIds = PersonRoleGroupId.rgTeamPlayer;
                    break;

                case PersonGroup.Medics:
                    personRoleIds = PersonRoleGroupId.rgMedicalStaff;
                    break;

                case PersonGroup.ReserveTeam:
                    personRoleIds = PersonRoleGroupId.rgTeamPlayer;
                    break;

                case PersonGroup.Specialists:
                    personRoleIds = PersonRoleGroupId.rgSpecialistsStuff;
                    break;

                case PersonGroup.Youth:
                    personRoleIds = PersonRoleGroupId.rgTeamPlayer;
                    break;
            }

            return DalPerson.GetTeamPersons(teamId, personRoleIds)
                            .Where(p => p.personStatusId == PersonStatusId.psContract
                                    || p.personStatusId == PersonStatusId.psRentFrom);
        }

        public IEnumerable<Person> GetTeamPersons(int teamId, DateTime date)
        {
            return DalPerson.GetTeamPersons(teamId, date);
        }

        public int SavePerson(Person entity)
        {
            return DalPerson.SavePerson(entity);
        }
    }
}

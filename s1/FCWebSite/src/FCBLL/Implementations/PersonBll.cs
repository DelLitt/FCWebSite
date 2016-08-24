namespace FCBLL.Implementations
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
        public bool FillTeams
        {
            get
            {
                return DalPerson.FillTeams;
            }

            set
            {
                DalPerson.FillTeams = value;
            }
        }

        public bool FillCities
        {
            get
            {
                return DalPerson.FillCities;
            }

            set
            {
                DalPerson.FillCities = value;
            }
        }

        public bool FillPersonRoles
        {
            get
            {
                return DalPerson.FillPersonRoles;
            }

            set
            {
                DalPerson.FillPersonRoles = value;
            }
        }

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
                                    .Union(new[] { PersonRoleId.rrVideographer, PersonRoleId.rrClubAdministrator, PersonRoleId.rrTeamManager });
                    break;

                case PersonGroup.Direction:
                    personRoleIds = new List<int>(PersonRoleGroupId.rgSeniorStaff)
                                    .Where(l => l != PersonRoleId.rrClubAdministrator && l != PersonRoleId.rrTeamManager)
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
                    personRoleIds = PersonRoleGroupId.rgSpecialistsStuff
                                    .Where(l => l != PersonRoleId.rrVideographer && l != PersonRoleId.rrAccountantChief);
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

        public IEnumerable<Person> GetPersons(IEnumerable<int> personsIds)
        {
            return DalPerson.GetPersons(personsIds);
        }

        public IEnumerable<Person> GetPersons()
        {
            return DalPerson.GetPersons();
        }

        public IEnumerable<Person> SearchByDefault(string text)
        {
            return DalPerson.SearchByDefault(text);
        }

        public int SavePerson(Person entity)
        {
            return DalPerson.SavePerson(entity);
        }
    }
}

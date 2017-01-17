namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Extensions;
    using FCCore.Model;

    public class PersonDal : DalBase, IPersonDal
    {
        public bool FillTeams { get; set; } = false;
        public bool FillCities { get; set; } = false;
        public bool FillPersonRoles { get; set; } = false;
        public bool FillPersonCareer { get; set; } = false;

        public Person GetPerson(int id)
        {
            Person person = Context.Person.FirstOrDefault(p => p.Id == id);

            if(person == null) { return null; }

            FillRelations(new Person[] { person });

            return person;
        }

        public IEnumerable<Person> GetTeamPersons(int teamId)
        {
            IEnumerable<Person> persons = Context.Person.Where(p => p.teamId == teamId).ToList();

            FillRelations(persons);

            return persons;
        }

        public IEnumerable<Person> GetTeamPersons(int teamId, IEnumerable<int> personRoleIds)
        {
            if (Guard.IsEmptyIEnumerable(personRoleIds)) { return new Person[0]; }

            IEnumerable<Person> persons = 
                Context.Person.Where(p => p.teamId == teamId && personRoleIds.Contains((int)p.roleId))
                .ToList();

            FillRelations(persons);

            return persons;
        }

        public IEnumerable<Person> GetTeamPersons(int teamId, DateTime date)
        {
            IEnumerable<Person> persons = 
                Context.Person.Where(p => p.PersonCareer.Any(pc => pc.teamId == teamId 
                                                              && pc.DateStart <= date 
                                                              && pc.DateFinish >= date));

            FillRelations(persons);

            return persons;
        }

        public IEnumerable<Person> GetPersons(IEnumerable<int> personsIds)
        {
            if (personsIds == null) { return new Person[0]; }

            IEnumerable<Person> persons = Context.Person.Where(r => personsIds.Contains(r.Id))
                                                        .AsEnumerable();

            FillRelations(persons);

            return persons.ToList();
        }

        public IEnumerable<Person> GetPersons()
        {
            return Context.Person.Take(LimitEntitiesSelections);
        }

        public IEnumerable<Person> SearchByDefault(string text)
        {
            IEnumerable<Person> persons = Context.Person.Where(p => p.GetNameDefault().Contains(text))
                                                        .AsEnumerable();

            return persons;
        }

        public int SavePerson(Person entity)
        {
            if (entity.Id > 0)
            {
                Context.Person.Update(entity);
            }
            else
            {
                Context.Person.Add(entity);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private void FillRelations(IEnumerable<Person> persons)
        {
            if (Guard.IsEmptyIEnumerable(persons)) { return; }

            IEnumerable<Team> teams = new Team[0];
            IEnumerable<City> cities = new City[0];
            IEnumerable<PersonRole> personRoles = new PersonRole[0];
            IEnumerable<PersonCareer> personCareers = new PersonCareer[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.FillCities = true;
                teamDal.SetContext(Context);

                var teamIds = new List<int>();
                teamIds.AddRange(persons.Where(p => p.teamId.HasValue).Select(p => p.teamId.Value).Distinct());

                teams = teamDal.GetTeams(teamIds).ToList();
            }

            if (FillCities)
            {
                var cityDal = new CityDal();
                cityDal.FillCountries = true;
                cityDal.SetContext(Context);

                var cityIds = new List<int>();
                cityIds.AddRange(persons.Where(p => p.cityId.HasValue).Select(p => p.cityId.Value).Distinct());

                cities = cityDal.GetCities(cityIds).ToList();
            }

            if (FillPersonRoles)
            {
                var personRoleDal = new PersonRoleDal();
                personRoleDal.SetContext(Context);

                var personRoleIds = new List<int>();
                personRoleIds.AddRange(persons.Where(p => p.roleId.HasValue).Select(p => (int)p.roleId.Value).Distinct());

                personRoles = personRoleDal.GetPersonRoles(personRoleIds).ToList();
            }

            if (FillPersonCareer)
            {
                var personCareerDal = new PersonCareerDal();
                personCareerDal.SetContext(Context);

                var personIds = new List<int>();
                personIds.AddRange(persons.Select(p => p.Id).Distinct());

                personCareers = personCareerDal.GetPersonCareer(personIds).ToList();
            }

            if (teams.Any() || cities.Any() || personRoles.Any() || personCareers.Any())
            {
                foreach (Person person in persons)
                {
                    if (FillTeams && teams.Any())
                    {
                        person.team = teams.FirstOrDefault(t => t.Id == person.teamId);
                        if (person.teamId.HasValue && person.team == null)
                        {
                            throw new DalMappingException(nameof(person.team), typeof(Person));
                        }
                    }

                    if (FillCities && cities.Any())
                    {
                        person.city = cities.FirstOrDefault(c => c.Id == person.cityId);
                        if (person.cityId.HasValue && person.city == null)
                        {
                            throw new DalMappingException(nameof(person.city), typeof(Person));
                        }
                    }

                    if (FillPersonRoles && personRoles.Any())
                    {
                        person.role = personRoles.FirstOrDefault(pr => pr.Id == person.roleId);
                        if (person.roleId.HasValue && person.role == null)
                        {
                            throw new DalMappingException(nameof(person.role), typeof(Person));
                        }
                    }

                    if (FillPersonCareer && personCareers.Any())
                    {
                        person.PersonCareer = personCareers.Where(pc => pc.personId == person.Id).ToList();
                    }
                }
            }
        }
    }
}

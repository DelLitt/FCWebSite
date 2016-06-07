namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using Exceptions;

    public class TeamDal : DalBase, ITeamDal
    {
        public bool FillCities { get; set; } = false;

        public Team GetTeam(int id)
        {
            Team team = Context.Team.FirstOrDefault(p => p.Id == id);

            if (team != null)
            {
                FillRelations(new Team[] { team });
            }

            return team;
        }

        public IEnumerable<Team> GetTeams(IEnumerable<int> ids)
        {
            if(Guard.IsEmptyIEnumerable(ids)) { return new Team[0]; }

            IEnumerable<Team> teams = Context.Team.Where(t => ids.Contains(t.Id));

            FillRelations(teams);

            return teams;
        }

        public IEnumerable<Team> GetAll()
        {
            IEnumerable<Team> teams = Context.Team.ToList();

            FillRelations(teams);

            return teams;
        }

        public IEnumerable<Team> SearchByDefault(string text)
        {
            IEnumerable<Team> teams = Context.Team.Where(t => t.Name.Contains(text) || t.city.NameFull.Contains(text));

            FillRelations(teams);

            return teams;
        }

        public IEnumerable<Team> SearchByDefault(string text, IEnumerable<int> teamIds)
        {
            if(Guard.IsEmptyIEnumerable(teamIds)) { return new Team[0]; }

            IEnumerable<Team> teams = Context.Team.Where(t => teamIds.Contains(t.Id) 
                                        && (t.Name.Contains(text) || (t.city != null && t.city.NameFull.Contains(text))));

            FillRelations(teams);

            return teams;
        }

        public int SaveTeam(Team entity)
        {
            if (entity.Id > 0)
            {
                Context.Team.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Team.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private void FillRelations(IEnumerable<Team> teams)
        {
            if (Guard.IsEmptyIEnumerable(teams)) { return; }

            IEnumerable<City> cities = new City[0];

            if (FillCities)
            {
                var citiesDal = new CityDal();
                citiesDal.SetContext(Context);

                var citiesIds = new List<int>();
                citiesIds.AddRange(teams.Select(r => (int)r.cityId));

                cities = citiesDal.GetCities(citiesIds.Distinct()).ToList();
            }

            if (cities.Any())
            {
                foreach (Team team in teams)
                {
                    if (FillCities && cities.Any())
                    {
                        team.city = cities.FirstOrDefault(t => t.Id == team.cityId);

                        if (team.cityId.HasValue && team.city == null)
                        {
                            throw new DalMappingException(nameof(team.city), typeof(Stadium));
                        }
                    }
                }
            }
        }
    }
}

namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;
    using Microsoft.Data.Entity;

    public class TeamDal : DalBase, ITeamDal
    {
        public bool? Active { get; set; }
        public bool FillCities { get; set; } = false;
        public bool FillMainTourney { get; set; } = false;
        public bool FillStadium { get; set; } = false;
        public bool FillTeamType { get; set; } = false;

        public Team GetTeam(int id)
        {
            IQueryable<Team> teams = Context.Team
                                            .Where(t => t.Id == id);

            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            Team team = result.FirstOrDefault();

            return team;
        }

        public IEnumerable<Team> GetTeamsByType(int teamTypeId)
        {
            IQueryable<Team> teams = Context.Team
                                            .Where(t => t.teamTypeId == teamTypeId);
           
            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            return result;
        }

        // TODO: Currently parentId is WebSite. Needs to be added new column!!!
        public IEnumerable<Team> GetTeamsByTypeAndParent(int teamTypeId, int parentTeamId)
        {
            string parentTeam = parentTeamId.ToString();

            IQueryable<Team> teams = Context.Team
                                            .Where(t => t.teamTypeId == teamTypeId
                                                     && t.WebSite == parentTeam);

            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            return result;
        }

        public IEnumerable<Team> GetTeams(IEnumerable<int> ids)
        {
            if(Guard.IsEmptyIEnumerable(ids)) { return new Team[0]; }

            IQueryable<Team> teams = Context.Team
                                            .Where(t => ids.Contains(t.Id));

            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            return result;
        }

        public IEnumerable<Team> GetTeams()
        {
            IQueryable<Team> teams = Context.Team;

            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            return result;
        }

        public IEnumerable<Team> SearchByDefault(string text)
        {
            IQueryable<Team> teams = Context.Team
                                            .Where(t => t.Name.Contains(text) 
                                                     || t.city.NameFull.Contains(text));

            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            return result;
        }

        public IEnumerable<Team> SearchByDefault(string text, IEnumerable<int> teamIds)
        {
            if(Guard.IsEmptyIEnumerable(teamIds)) { return new Team[0]; }

            IQueryable<Team> teams = Context.Team
                                            .Where(t => teamIds.Contains(t.Id) 
                                                        && (t.Name.Contains(text) 
                                                            || (t.city != null 
                                                                && t.city.NameFull.Contains(text))));

            IEnumerable<Team> result = ApplySettings(teams);

            FillRelations(result);

            return result;
        }

        public int SaveTeam(Team entity)
        {
            if (entity.Id > 0)
            {
                Context.Team.Update(entity, GraphBehavior.SingleObject);
            }
            else
            {
                Context.Team.Add(entity, GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private IEnumerable<Team> ApplySettings(IQueryable<Team> teams)
        {
            if (Active.HasValue)
            {
                teams = teams.Where(t => t.Active == Active.Value);
            }

            if (FillCities)
            {
                teams = teams.Include(t => t.city);
            }

            if (FillMainTourney)
            {
                teams = teams.Include(t => t.mainTourney);
            }

            if (FillStadium)
            {
                teams = teams.Include(t => t.stadium);
            }

            if (FillTeamType)
            {
                teams = teams.Include(t => t.teamType);
            }

            teams = teams.Take(LimitEntitiesSelections);

            return teams.ToList();
        }

        private void FillRelations(IEnumerable<Team> teams)
        {
            //if (Guard.IsEmptyIEnumerable(teams)) { return; }

            //IEnumerable<City> cities = new City[0];

            //if (FillCities)
            //{
            //    var citiesDal = new CityDal();
            //    citiesDal.SetContext(Context);

            //    var citiesIds = new List<int>();
            //    citiesIds.AddRange(teams.Select(r => (int)r.cityId));

            //    cities = citiesDal.GetCities(citiesIds.Distinct()).ToList();
            //}

            //if (cities.Any())
            //{
            //    foreach (Team team in teams)
            //    {
            //        if (FillCities && cities.Any())
            //        {
            //            team.city = cities.FirstOrDefault(t => t.Id == team.cityId);

            //            if (team.cityId.HasValue && team.city == null)
            //            {
            //                throw new DalMappingException(nameof(team.city), typeof(Stadium));
            //            }
            //        }
            //    }
            //}
        }
    }
}

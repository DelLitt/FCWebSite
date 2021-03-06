﻿namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;

    public class PersonCareerDal : DalBase, IPersonCareerDal
    {
        public bool FillTeams { get; set; } = false;

        public IEnumerable<PersonCareer> GetPersonCareer(int personId)
        {
            IEnumerable<PersonCareer> result = Context.PersonCareer.Where(pc => pc.personId == personId);

            FillRelations(result);

            return result;
        }

        public IEnumerable<PersonCareer> GetPersonCareer(IEnumerable<int> personId)
        {
            IEnumerable<PersonCareer> result = Context.PersonCareer.Where(pc => personId.Contains(pc.personId));

            FillRelations(result);

            return result;
        }

        public IEnumerable<int> SavePersonCareer(IEnumerable<PersonCareer> entities)
        {
            if(Guard.IsEmptyIEnumerable(entities)) { return new int[0]; }

            var result = new List<int>();
            IEnumerable<int> saveIds = entities.Select(e => e.Id);

            IEnumerable<PersonCareer> removeItems = 
                Context.PersonCareer.Where(pc => pc.personId == entities.First().personId && !saveIds.Contains(pc.Id));

            Context.PersonCareer.RemoveRange(removeItems);

            foreach (PersonCareer entity in entities)
            {
                if (entity.Id > 0)
                {
                    Context.PersonCareer.Update(entity);
                }
                else
                {
                    Context.PersonCareer.Add(entity);
                }                
            }

            Context.SaveChanges();

            result.AddRange(entities.Select(e => e.Id));

            return result;
        }

        private void FillRelations(IEnumerable<PersonCareer> personCareers)
        {
            IEnumerable<Team> teams = new Team[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.SetContext(Context);

                var teamIds = new List<int>();
                teamIds.AddRange(personCareers.Select(r => r.teamId).Distinct());

                teams = teamDal.GetTeams(teamIds).ToList();
            }

            if (teams.Any())
            {
                foreach (PersonCareer pc in personCareers)
                {
                    if (FillTeams && teams.Any())
                    {
                        pc.team = teams.FirstOrDefault(t => t.Id == pc.teamId);

                        if (pc.team == null)
                        {
                            throw new DalMappingException(nameof(pc.team), typeof(PersonCareer));
                        }
                    }
                }
            }
        }
    }
}

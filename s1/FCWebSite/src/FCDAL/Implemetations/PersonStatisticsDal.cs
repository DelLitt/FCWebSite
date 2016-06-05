namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PersonStatisticsDal : DalBase, IPersonStatisticsDal
    {
        public bool FillTeams { get; set; } = false;
        public bool FillTourneys { get; set; } = false;

        public IEnumerable<PersonStatistics> GetPersonStatistics(int personId)
        {
            IEnumerable<PersonStatistics> personStatistics = Context.PersonStatistics.Where(ps => ps.personId == personId);

            FillRelations(personStatistics);

            return personStatistics;
        }

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId)
        {            
            IEnumerable<PersonStatistics> result = Context.PersonStatistics.Where(ps => ps.teamId == temaId && ps.tourneyId == tourneyId);

            FillRelations(result);

            return result;
        }

        public int SavePersonStatistics(PersonStatistics entity)
        {
            if (entity.Id > 0)
            {
                Context.PersonStatistics.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.PersonStatistics.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private void FillRelations(IEnumerable<PersonStatistics> personStatistics)
        {
            IEnumerable<Team> teams = new Team[0];
            IEnumerable<Tourney> toureys = new Tourney[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.SetContext(Context);

                var teamIds = new List<int>();
                teamIds.AddRange(personStatistics.Select(r => r.teamId).Distinct());

                teams = teamDal.GetTeams(teamIds).ToList();
            }

            if (FillTourneys)
            {
                var tourneyDal = new TourneyDal();
                tourneyDal.SetContext(Context);

                var tourneyIds = new List<int>();
                tourneyIds.AddRange(personStatistics.Select(r => (int)r.tourneyId).Distinct());

                toureys = tourneyDal.GetTourneys(tourneyIds).ToList();
            }

            if (teams.Any() || toureys.Any())
            {
                foreach (PersonStatistics ps in personStatistics)
                {
                    if (FillTeams && teams.Any())
                    {
                        ps.team = teams.FirstOrDefault(t => t.Id == ps.teamId);

                        if (ps.team == null)
                        {
                            throw new DalMappingException(nameof(ps.team), typeof(PersonStatistics));
                        }
                    }

                    if (FillTourneys && toureys.Any())
                    {
                        ps.tourney = toureys.FirstOrDefault(t => t.Id == ps.tourneyId);

                        if (ps.tourney == null)
                        {
                            throw new DalMappingException(nameof(ps.tourney), typeof(PersonStatistics));
                        }
                    }
                }
            }
        }
    }
}

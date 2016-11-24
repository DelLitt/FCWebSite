namespace FCDAL.Implementations
{
    using System;
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

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int teamId, int tourneyId)
        {            
            IEnumerable<PersonStatistics> result = Context.PersonStatistics.Where(ps => ps.teamId == teamId && ps.tourneyId == tourneyId);

            FillRelations(result);

            return result;
        }

        public IEnumerable<PersonStatistics> GetPersonsStatistics(int tourneyId, IEnumerable<int> personIds)
        {
            IEnumerable<PersonStatistics> result = Context.PersonStatistics
                .Where(ps => ps.tourneyId == tourneyId && ps.tourneyId == tourneyId && personIds.Contains(ps.personId)).ToList();

            FillRelations(result);

            return result;
        }

        public int SavePersonStatistics(int tourneyId, IEnumerable<PersonStatistics> personStatistics)
        {
            if (personStatistics == null) { return -1; }            

            List<PersonStatistics> saveRecords = personStatistics.Where(r => r.tourneyId == tourneyId).ToList();
            List<PersonStatistics> dbRecords = GetPersonsStatistics(tourneyId, saveRecords.Select(r => r.personId)).ToList();
            var insertRecords = new List<PersonStatistics>();
            IEnumerable<PersonStatistics> removeRecords = new PersonStatistics[] { };

            int removeCount = dbRecords.Count - saveRecords.Count;
            if (removeCount > 0)
            {
                removeRecords = dbRecords.Skip(saveRecords.Count);
                Context.RemoveRange(removeRecords);
            }

            for (int i = 0; i < saveRecords.Count; i++)
            {
                PersonStatistics dbRecord = dbRecords.ElementAtOrDefault(i);

                if (dbRecord == null)
                {
                    dbRecord = new PersonStatistics();
                    insertRecords.Add(dbRecord);
                }

                dbRecord.Assists = saveRecords[i].Assists;
                dbRecord.CustomIntValue = saveRecords[i].CustomIntValue;
                dbRecord.Games = saveRecords[i].Games;
                dbRecord.Goals = saveRecords[i].Goals;
                dbRecord.personId = saveRecords[i].personId;
                dbRecord.Reds = saveRecords[i].Reds;
                dbRecord.Substitutes = saveRecords[i].Substitutes;
                dbRecord.teamId = saveRecords[i].teamId;
                dbRecord.tourneyId = saveRecords[i].tourneyId;
                dbRecord.Yellows = saveRecords[i].Yellows;
            }

            Context.PersonStatistics.AddRange(insertRecords, Microsoft.Data.Entity.GraphBehavior.SingleObject);

            try
            {
                //foreach (PersonStatistics ps in insertRecords)
                //{
                //    Context.PersonStatistics.AddRange(insertRecords, Microsoft.Data.Entity.GraphBehavior.SingleObject);
                //    Context.SaveChanges();
                //}

                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                throw;
            }
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

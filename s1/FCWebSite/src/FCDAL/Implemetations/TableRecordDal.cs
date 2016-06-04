namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using Exceptions;
    using FCCore.Abstractions.Dal;

    public class TableRecordDal : DalBase, ITableRecordDal
    {
        public bool FillTeams { get; set; } = false;
        public bool FillTourney { get; set; } = false;

        public IEnumerable<TableRecord> GetTourneyTable(int tourneyId)
        {
            IEnumerable<TableRecord> records = Context.TableRecord.Where(tr => tr.tourneyId == tourneyId).ToList();

            FillRelations(records);

            return records;
        }

        private void FillRelations(IEnumerable<TableRecord> tableRecords)
        {
            Tourney tourney = null;
            IEnumerable<Team> teams = new Team[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.SetContext(Context);

                teams = teamDal.GetTeams(tableRecords.Select(r => r.teamId));

                if (!teams.Any())
                {
                    throw new DalMappingException(nameof(teams), typeof(TableRecord));
                }
            }

            if (FillTourney)
            {
                var tourneyDal = new TourneyDal();
                tourneyDal.SetContext(Context);

                tourney = tourneyDal.GetTourney(tableRecords.First().tourneyId);

                if (tourney == null)
                {
                    throw new DalMappingException(nameof(tourney), typeof(TableRecord));
                }
            }

            if (teams.Any() || tourney != null)
            {
                foreach (TableRecord record in tableRecords)
                {
                    if (FillTeams)
                    {
                        record.Team = teams.FirstOrDefault(t => t.Id == record.teamId);

                        if (record.Team == null)
                        {
                            throw new DalMappingException(nameof(record.Team), typeof(TableRecord));
                        }
                    }

                    record.tourney = FillTourney ? tourney : null;
                }
            }
        }
    }
}

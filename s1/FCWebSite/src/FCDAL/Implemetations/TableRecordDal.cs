namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    public class TableRecordDal : DalBase, ITableRecordDal
    {
        public bool FillTeams { get; set; } = false;

        public bool FillTourney { get; set; } = false;

        public IEnumerable<TableRecord> GetTourneyTable(int tourneyId)
        {
            IEnumerable<TableRecord> records = Context.TableRecord.Where(tr => tr.tourneyId == tourneyId).ToList();

            if(!Guard.IsEmptyIEnumerable(records))
            {
                Tourney tourney = null;
                IEnumerable<Team> teams = new Team[0];

                if (FillTeams)
                {
                    var teamDal = new TeamDal();
                    teamDal.SetContext(Context);

                    teams = teamDal.GetTeams(records.Select(r => r.teamId));

                    if (!teams.Any())
                    {
                        throw new DalMappingException(nameof(teams), typeof(TableRecord));
                    }
                }

                if (FillTourney)
                {
                    var tourneyDal = new TourneyDal();
                    tourneyDal.SetContext(Context);

                    tourney = tourneyDal.GetTourney(records.First().tourneyId);

                    if(tourney == null)
                    {
                        throw new DalMappingException(nameof(tourney), typeof(TableRecord));
                    }
                }

                if (teams.Any() || tourney != null)
                {
                    foreach (TableRecord record in records)
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

            return records;
        }
    }
}

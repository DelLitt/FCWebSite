namespace FCBLL.Ranking.Standings.Decorators
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;

    public class TableByAlphabet : TableDecorator
    {
        public TableByAlphabet(TableBase table) : base(table)
        {
        }

        protected override void CalculatePriorities(IEnumerable<TableRecord> records)
        {
            records = SortByName(records);

            int recordsCount = records.Count();
            for (short i = 1; i <= recordsCount; i++)
            {
                records.ElementAt(i - 1).PointsVirtual = i;
            }
        }

        protected IEnumerable<TableRecord> SortByName(IEnumerable<TableRecord> records)
        {
            if(Guard.IsEmptyIEnumerable(records)) { return new TableRecord[] { }; }

            if (records.Any(rg => rg.Team == null))
            {
                var teamBll = MainCfg.ServiceProvider.GetService<ITeamBll>();

                IEnumerable<int> teamIds = records.Where(rg => rg.Team == null).Select(rg => rg.teamId);
                if (teamIds.Any())
                {
                    IEnumerable<Team> teams = teamBll.GetTeams(teamIds);
                    if (teams.Any())
                    {
                        foreach (TableRecord record in records)
                        {
                            if (record.Team == null)
                            {
                                record.Team = teams.FirstOrDefault(t => t.Id == record.teamId);
                            }
                        }
                    }
                }
            }

            if (records.Any(rg => rg.Team == null))
            {
                foreach (TableRecord record in records)
                {
                    if (record.Team == null)
                    {
                        record.Team = new Team() { Name = string.Empty };
                    }
                }
            }

            return records.OrderByDescending(r => r.Team.Name);
        }
    }
}

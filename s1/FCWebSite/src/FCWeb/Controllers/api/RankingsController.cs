namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Components;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class RankingsController : Controller
    {
        private ITableRecordBll tableRecordBll { get; set; }
        private ITourneyBll tourneyBll { get; set; }
        private IRanking ranking { get; set; }

        public RankingsController(ITableRecordBll tableRecordBll, ITourneyBll tourneyBll, IRanking ranking)
        {
            this.tableRecordBll = tableRecordBll;
            this.tourneyBll = tourneyBll;
            this.ranking = ranking;
        }

        [HttpGet("{id:int}")]
        public RankingTableViewModel Get(int id)
        {
            Tourney tourney = tourneyBll.GetTourney(id);

            if (tourney == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            string tourneyName = tourney?.Name ?? string.Empty;

            tableRecordBll.FillTeams = true;
            return tableRecordBll.GetTourneyTable(id).ToViewModel(tourneyName);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public RankingTableViewModel Put(int id)
        {
            Tourney tourney = tourneyBll.GetTourney(id);

            if (tourney == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            IEnumerable<TableRecord> tableRecords = ranking.CalculateTable(id);

            if(!tableRecords.Any())
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            tableRecordBll.SaveTourneyTable(tourney.Id, tableRecords);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"DELETE FROM [dbo].[TableRecords] WHERE [tournamentId] = {tourney.Id};");
            sb.AppendLine();
            sb.AppendLine("INSERT INTO [dbo].[TableRecords]");
            sb.AppendLine("([teamId],[tournamentId],[games],[wins],[draws],[loses],[goalsFor],[goalsAgainst],[points],[virtualPoints],[active],[position])");
            sb.AppendLine("VALUES");

            List<string> values = new List<string>();

            foreach (TableRecord tr in tableRecords)
            {
                int active = tr.Active ? 1 : 0;

                values.Add($"({tr.teamId},{tr.tourneyId},{tr.Games},{tr.Wins},{tr.Draws},{tr.Loses},{tr.GoalsFor},{tr.GoalsAgainst},{tr.Points}," +
                    $"{tr.PointsVirtual},{active},{tr.Position})");
            }

            sb.AppendLine(string.Join(",", values));

            System.IO.File.WriteAllText("D:\\updTable.sql", sb.ToString());

            return Get(tourney.Id);
        }
    }
}

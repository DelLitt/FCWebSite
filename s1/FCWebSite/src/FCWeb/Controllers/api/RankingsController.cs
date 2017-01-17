﻿namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
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
        //[FromServices]
        private ITableRecordBll tableRecordBll { get; set; }
        //[FromServices]
        private ITourneyBll tourneyBll { get; set; }
        //[FromServices]
        private IRanking ranking { get; set; }

        public RankingsController(ITableRecordBll tableRecordBll, ITourneyBll tourneyBll, IRanking ranking)
        {
            this.tableRecordBll = tableRecordBll;
            this.tourneyBll = tourneyBll;
            this.ranking = ranking;
        }

        // GET api/values/5
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

        // PUT api/values/5
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

            string tourneyName = tourney?.Name ?? string.Empty;

            //ranking = MainCfg.ServiceProvider.GetService<IRanking>();

            IEnumerable<TableRecord> tableRecords = ranking.CalculateTable(id);

            if(!tableRecords.Any())
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            tableRecordBll.SaveTourneyTable(tourney.Id, tableRecords);

            return Get(tourney.Id);
        }
    }
}

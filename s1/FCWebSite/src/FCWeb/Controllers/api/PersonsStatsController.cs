namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using FCCore.Abstractions.Bll;
    using Core.Extensions;
    using ViewModels;
    using FCCore.Configuration;

    [Route("api/[controller]")]
    public class PersonsStatsController : Controller
    {
        private IPersonStatisticsBll personStatisticsBll { get; set; }

        public PersonsStatsController(IPersonStatisticsBll personStatisticsBll)
        {
            this.personStatisticsBll = personStatisticsBll;
        }

        [HttpGet("{personId}")]
        public IEnumerable<PersonStatisticsViewModel> Get(int personId)
        {
            personStatisticsBll.FillTeams = true;
            personStatisticsBll.FillTourneys = true;

            return personStatisticsBll.GetPersonStatistics(personId).ToViewModel();
        }

        [HttpGet("team/{teamId}/tourney/{tourneyId}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "teamId", "tourneyId" }, Duration = Constants.Cache_LongVaryByParamDurationSeconds)]
        public IEnumerable<PersonStatisticsViewModel> Get(int teamId, int tourneyId)
        {
            return personStatisticsBll.GetPersonsStatistics(teamId, tourneyId).ToViewModel();
        }
    }
}

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using Core.Extensions;
    using ViewModels;

    [Route("api/[controller]")]
    public class PersonsStatsController : Controller
    {
        //[FromServices]
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
        public IEnumerable<PersonStatisticsViewModel> Get(int teamId, int tourneyId)
        {
            return personStatisticsBll.GetPersonsStatistics(teamId, tourneyId).ToViewModel();
        }
    }
}

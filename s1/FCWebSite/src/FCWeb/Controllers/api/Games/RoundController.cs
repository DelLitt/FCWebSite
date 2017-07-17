namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ViewModels;

    [Route("api/games/[controller]")]
    public class RoundController : Controller
    {
        private IGameBll gameBll { get; set; }
        private IRoundBll roundBll { get; set; }
        private ILogger<RoundController> logger { get; set; }

        public RoundController(IGameBll gameBll, IRoundBll roundBll, ILogger<RoundController> logger)
        {
            this.gameBll = gameBll;
            this.roundBll = roundBll;
            this.logger = logger;
        }

        // /api/games/round/32/mode/1
        [HttpGet("{id:int}/mode/{mode:int}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "id", "mode" }, Duration = Constants.Cache_DefaultVaryByParamDurationSeconds)]
        public RoundInfoViewModel Get(int id, int mode)
        {
            if (mode == 1)
            {
                gameBll.FillRounds = true;
                gameBll.FillTeams = true;
                gameBll.FillTourneys = true;

                IEnumerable<RoundInfoViewModel> roundViews = gameBll.GetRoundGames(id).ToRoundInfoViewModel();

                return roundViews.FirstOrDefault();
            }

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            logger.LogWarning("Round info of the game does not support mode={0}!", mode);
            return null;
        }

        // /api/games/round/team/3/slider?tourneyIds=8&tourneyIds=10
        [HttpGet("team/{teamId}/slider")]
        [ResponseCache(VaryByQueryKeys = new string[] { "teamId", "tourneyIds" }, Duration = Constants.Cache_DefaultVaryByParamDurationSeconds)]
        public IEnumerable<RoundSliderViewModel> Get(int teamId, [FromQuery] int[] tourneyIds)
        {
            var date = DateTime.UtcNow;
            var actualDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

            logger.LogTrace("Getting schedule. Tournaments count: {0}.", tourneyIds.Count());

            MethodInfo methodInfo = typeof(RoundsSliderHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(RoundsSliderHelper.GetRoundsSlider));

            string cacheKey = roundBll.ObjectKeyGenerator.GetStringKey(methodInfo, teamId, tourneyIds, actualDate);

            IEnumerable<RoundSliderViewModel> result =
                roundBll.Cache.GetOrCreate(cacheKey, () => { return RoundsSliderHelper.GetRoundsSlider(teamId, tourneyIds, actualDate); });

            return result;
        }
    }
}

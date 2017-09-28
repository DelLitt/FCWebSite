namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions;
    using FCCore.Abstractions.Bll;
    using FCCore.Caching;
    using FCCore.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ViewModels.Game;
    using ViewModels.Schedule;

    [Route("api/games/[controller]")]
    public class ScheduleController : Controller
    {
        private IFCCache cache { get; set; }
        private IObjectKeyGenerator cacheKeyGenerator { get; set; }
        private ILogger<ScheduleController> logger { get; set; }
        private IGameBll gameBll { get; set; }

        public ScheduleController(IFCCache cache, IGameBll gameBll, IObjectKeyGenerator cacheKeyGenerator, ILogger<ScheduleController> logger)
        {
            this.cache = cache;
            this.gameBll = gameBll;
            this.cacheKeyGenerator = cacheKeyGenerator;
            this.logger = logger;
        }

        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new string[] { "tourneyIds", "start", "end" }, Duration = Constants.Cache_DefaultVaryByParamDurationSeconds)]
        public IEnumerable<ScheduleItemViewModel> Get([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] int[] tourneyIds)
        {
            logger.LogTrace("Getting schedule. Start: {0}. End: {1}. Tournaments count: {2}.", start, end, tourneyIds.Count());

            MethodInfo methodInfo = typeof(ScheduleHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(ScheduleHelper.GetTourneysShcedule));

            string cacheKey = cacheKeyGenerator.GetStringKey(methodInfo, start, end, tourneyIds);

            IEnumerable<ScheduleItemViewModel> result =
                cache.GetOrCreate(cacheKey, () => { return ScheduleHelper.GetTourneysShcedule(start, end, tourneyIds); });

            return result;
        }

        [HttpGet("tournament/{id:int}/{mode}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "id", "mode", "teamIds" }, Duration = Constants.Cache_MiddleVaryByParamDurationSeconds)]
        public IEnumerable<GameQuickInfoViewModel> Get(int id, string mode, [FromQuery] int[] teamIds)
        {
            gameBll.FillTeams = true;
            gameBll.FillTourneys = true;
            gameBll.FillRounds = true;
            gameBll.FillStadiums = true;

            logger.LogTrace("Getting tournament {0} schedule. Mode: {1}. Teams count: {2}.", id, mode, teamIds != null ? string.Join(",", teamIds) : "All");

            int daysShift = mode.Equals("quick", StringComparison.OrdinalIgnoreCase) ? MainCfg.TeamGamesInfoDaysShift : MainCfg.MaxGamesInfoDaysShift;

            IEnumerable<GameQuickInfoViewModel> games = gameBll.GetTourneyGames(id, teamIds, DateTime.UtcNow, daysShift)
                                                               .OrderByDescending(g => g.GameDate)
                                                               .ToGameQuickInfoViewModel();

            return games;
        }
    }
}

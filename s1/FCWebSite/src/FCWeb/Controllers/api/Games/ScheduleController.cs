namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Core;
    using FCCore.Abstractions;
    using FCCore.Caching;
    using FCCore.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ViewModels.Schedule;

    [Route("api/games/[controller]")]
    public class ScheduleController : Controller
    {
        private IFCCache cache { get; set; }
        private IObjectKeyGenerator cacheKeyGenerator { get; set; }
        private ILogger<ScheduleController> logger { get; set; }

        public ScheduleController(IFCCache cache, IObjectKeyGenerator cacheKeyGenerator, ILogger<ScheduleController> logger)
        {            
            this.cache = cache;
            this.cacheKeyGenerator = cacheKeyGenerator;
            this.logger = logger;
        }

        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new string[] { "tourneyIds" }, Duration = Constants.Cache_DefaultVaryByParamDurationSeconds)]
        public IEnumerable<ScheduleItemViewModel> Get([FromQuery] int[] tourneyIds)
        {
            logger.LogTrace("Getting schedule. Tournaments count: {0}.", tourneyIds.Count());

            MethodInfo methodInfo = typeof(ScheduleHelper)
                                    .GetTypeInfo()
                                    .GetMethod(nameof(ScheduleHelper.GetTourneysShcedule));

            string cacheKey = cacheKeyGenerator.GetStringKey(methodInfo, tourneyIds);

            IEnumerable<ScheduleItemViewModel> result = 
                cache.GetOrCreate(cacheKey, () => { return ScheduleHelper.GetTourneysShcedule(tourneyIds); });

            return result;
        }
    }
}

namespace FCWeb.Controllers.Api.Union
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ViewModels;
    using ViewModels.Union;

    [Route("api/union/[controller]")]
    public class MainPageController : Controller
    {
        private IPublicationBll publicationBll;
        private IImageGalleryBll imageGalleryBLL;
        private IVideoBll videoBll;
        private ITourneyBll tourneyBll;
        private ITableRecordBll tableRecordBll;
        private ILogger<MainPageController> logger;

        public MainPageController(IPublicationBll publicationBll, IImageGalleryBll imageGalleryBLL, IVideoBll videoBll, ITourneyBll tourneyBll, ITableRecordBll tableRecordBll, ILogger<MainPageController> logger)
        {
            this.publicationBll = publicationBll;
            this.imageGalleryBLL = imageGalleryBLL;
            this.videoBll = videoBll;
            this.tourneyBll = tourneyBll;
            this.tableRecordBll = tableRecordBll;
            this.logger = logger;
        }
        
        [HttpGet("{content}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "content" }, Duration = 180)]
        public object Get(string content)
        {
            logger.LogTrace("Getting union content '{0}' of the main page!", content);

            if (content.Equals("base", StringComparison.OrdinalIgnoreCase))
            {                
                return GetMainPageContentBase();
            }

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            logger.LogWarning("Union content '{0}' is not supported for the main page!", content);
            return null;
        }

        private MainPageContentBaseViewModel GetMainPageContentBase()
        {
            var mainPublications = new MainPageContentBaseViewModel()
            {
                imageGalleries = imageGalleryBLL.GetMainImageGalleries(MainCfg.MainGalleriesCount, 0).ToShortViewModel(),
                publications = publicationBll.GetMainPublications(MainCfg.MainPublicationsCount, 0).ToShortViewModel(),
                videos = videoBll.GetMainVideos(MainCfg.MainVideosCount, 0).ToShortViewModel(),
                rankingTable = GetRankingTable()
            };

            return mainPublications;
        }

        private IEnumerable<RankingTableViewModel> GetRankingTable()
        {
            int tourneyId = MainCfg.MainTableTourneyId;
            Tourney tourney = tourneyBll.GetTourney(tourneyId);

            if (tourney == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                logger.LogWarning("Ranking table of tourney ID='{0}' is not found!", tourneyId);
                return null;
            }

            string tourneyName = tourney?.Name ?? string.Empty;

            tableRecordBll.FillTeams = true;
            RankingTableViewModel mainTable = tableRecordBll.GetTourneyTable(tourneyId).ToViewModel(tourneyName);

            return new RankingTableViewModel[] { mainTable };
        }
    }
}

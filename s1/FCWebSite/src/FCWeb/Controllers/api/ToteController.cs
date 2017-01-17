namespace FCWeb.Controllers.Api
{
    using System.Net;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    [Route("api/game/{gameId}/[controller]")]
    public class ToteController : Controller
    {
        //[FromServices]
        private ITotalizatorBll toteBll { get; set; }

        public ToteController(ITotalizatorBll toteBll)
        {
            this.toteBll = toteBll;
        }

        [HttpGet("{check}")]
        public FCResultViewModel Get(int gameId, bool check)
        {
            if (!check) { return new FCResultViewModel() { success = false }; }

            IPAddress remoteIpAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;

            if(remoteIpAddress == null) { return new FCResultViewModel() { success = false }; }

            return new FCResultViewModel()
            {
                success = toteBll.IsUserVoted(gameId, remoteIpAddress.ToString())
            };            
        }

        [HttpGet]
        public ToteResult Get(int gameId)
        {
            return toteBll.GetResult(gameId);
        }

        [HttpPost]
        public ToteResult Post(int gameId, [FromBody]short voteType)
        {
            IPAddress remoteIpAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;

            if (remoteIpAddress == null) { return new ToteResult(); }

            if (toteBll.IsUserVoted(gameId, remoteIpAddress.ToString()))
            {
                return toteBll.GetResult(gameId);
            }

            return toteBll.AddVote(gameId, voteType, remoteIpAddress.ToString());
        }
    }
}

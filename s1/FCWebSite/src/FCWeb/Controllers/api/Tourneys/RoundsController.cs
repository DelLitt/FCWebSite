//namespace FCWeb.Controllers.Api.Tourneys
//{
//    using System.Collections.Generic;
//    using Core.Extensions;
//    using FCCore.Abstractions.Bll;
//    using Microsoft.AspNet.Mvc;
//    using ViewModels;

//    [Route("api/tourneys/[controller]")]
//    public class RoundsController : Controller
//    {
//        //[FromServices]
//        private ITourneyBll tourneyBll { get; set; }

//        //[FromServices]
//        private IRoundBll roundBll { get; set; }

//        public RoundsController(ITourneyBll tourneyBll, IRoundBll roundBll)
//        {
//            this.tourneyBll = tourneyBll;
//            this.roundBll = roundBll;
//        }

//        // GET: api/values
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public TourneyViewModel Get(int id)
//        {
//            return tourneyBll.GetTourneyByRoundId(id).ToViewModel();
//        }

//        //// POST api/values
//        //[HttpPost]
//        //public void Post([FromBody]string value)
//        //{
//        //}

//        //// PUT api/values/5
//        //[HttpPut("{id}")]
//        //public void Put(int id, [FromBody]string value)
//        //{
//        //}

//        //// DELETE api/values/5
//        //[HttpDelete("{id}")]
//        //public void Delete(int id)
//        //{
//        //}
//    }
//}

namespace FCWeb.Controllers.Api.Rounds
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        //[FromServices]
        private IEventBll eventBll { get; set; }

        public EventsController(IEventBll eventBll)
        {
            this.eventBll = eventBll;
        }

        [HttpGet]
        public IEnumerable<EventViewModel> Get()
        {
            return eventBll.GetAll().ToViewModel();
        }

        [HttpGet("{id:int}")]
        public EventViewModel Get(int id)
        {
            return eventBll.GetEvent(id).ToViewModel();
        }

        [HttpGet("search")]
        public IEnumerable<EventViewModel> Get([FromQuery] string txt)
        {
            return eventBll.SearchByDefault(txt).ToViewModel();
        }
    }
}

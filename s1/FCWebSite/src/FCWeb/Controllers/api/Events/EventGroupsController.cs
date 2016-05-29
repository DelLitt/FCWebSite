namespace FCWeb.Controllers.Api.Rounds
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class EventGroupsController : Controller
    {
        //[FromServices]
        private IEventGroupBll eventGroupBll { get; set; }

        public EventGroupsController(IEventGroupBll eventGroupBll)
        {
            this.eventGroupBll = eventGroupBll;
        }

        [HttpGet]
        public IEnumerable<EventGroupViewModel> Get()
        {
            return eventGroupBll.GetAll().ToViewModel();
        }

        [HttpGet("{id:int}")]
        public EventGroupViewModel Get(int id)
        {
            return eventGroupBll.GetEventGroup(id).ToViewModel();
        }

        [HttpGet("filter")]
        public IEnumerable<EventGroupViewModel> Get([FromQuery] int[] ids)
        {
            return eventGroupBll.GetEventGroups(ids).ToViewModel();
        }

        [HttpGet("search")]
        public IEnumerable<EventGroupViewModel> Get([FromQuery] string txt)
        {
            return eventGroupBll.SearchByDefault(txt).ToViewModel();
        }
    }
}

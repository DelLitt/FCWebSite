namespace FCWeb.Controllers.Api.Rounds
{
    using System.Collections.Generic;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/events/{eventGroup}")]
    public class EventsGrouppedController : Controller
    {
        //[FromServices]
        private IEventBll eventBll { get; set; }

        public EventsGrouppedController(IEventBll eventBll)
        {
            this.eventBll = eventBll;
        }

        [HttpGet]
        public IEnumerable<EventViewModel> Get(string eventGroup)
        {
            int eventGroupId = EventHelper.GetIdByFiendlyName(eventGroup);

            return eventBll.GetAllByGroup(eventGroupId).ToViewModel();
        }

        [HttpGet("search")]
        public IEnumerable<EventViewModel> Get(string eventGroup, [FromQuery] string txt)
        {
            int eventGroupId = EventHelper.GetIdByFiendlyName(eventGroup);

            return eventBll.SearchByDefaultByGroup(eventGroupId, txt).ToViewModel();
        }
    }
}

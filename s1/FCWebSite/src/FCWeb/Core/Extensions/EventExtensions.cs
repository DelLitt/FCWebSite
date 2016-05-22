namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class EventExtensions
    {
        public static EventViewModel ToViewModel(this Event item)
        {
            if (item == null) { return null; }

            return new EventViewModel()
            {
                id = item.Id,
                name = item.Name,
                nameFull = item.NameFull,
                eventGroupId = item.eventGroupId                
            };
        }

        public static IEnumerable<EventViewModel> ToViewModel(this IEnumerable<Event> items)
        {
            if (Guard.IsEmptyIEnumerable(items)) { return new EventViewModel[0]; }

            return items.Select(v => v.ToViewModel()).ToList();
        }
    }
}

namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class EventGroupExtensions
    {
        public static EventGroupViewModel ToViewModel(this EventGroup item)
        {
            if (item == null) { return null; }

            return new EventGroupViewModel()
            {
                id = item.Id,
                name = item.Name,
                nameFull = item.NameFull         
            };
        }

        public static IEnumerable<EventGroupViewModel> ToViewModel(this IEnumerable<EventGroup> items)
        {
            if (Guard.IsEmptyIEnumerable(items)) { return new EventGroupViewModel[0]; }

            return items.Select(v => v.ToViewModel()).ToList();
        }
    }
}

namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using System;

    public class EventGroupDal : DalBase, IEventGroupDal
    {
        public EventGroup GetEventGroup(int id)
        {
            return Context.EventGroup.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<EventGroup> GetEventGroups(IEnumerable<int> ids)
        {
            if (ids == null) { return new EventGroup[0]; }

            IEnumerable<EventGroup> eventGroups = Context.EventGroup.Where(eg => ids.Contains(eg.Id));

            return eventGroups;
        }

        public IEnumerable<EventGroup> GetAll()
        {
            return Context.EventGroup;
        }

        public IEnumerable<EventGroup> SearchByDefault(string text)
        {
            return Context.EventGroup.Where(e => e.NameFull.Contains(text));
        }
    }
}

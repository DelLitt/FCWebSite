namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using System;

    public class EventDal : DalBase, IEventDal
    {
        public Event GetEvent(int id)
        {
            return Context.Event.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Event> GetEvents(IEnumerable<int> ids)
        {
            if (ids == null) { return new Event[0]; }

            return Context.Event.Where(e => ids.Contains(e.Id));
        }

        public IEnumerable<Event> GetAll()
        {
            return Context.Event;
        }

        public IEnumerable<Event> GetAllByGroup(int eventGroupId)
        {
            return Context.Event.Where(e => e.eventGroupId == eventGroupId);
        }

        public IEnumerable<Event> SearchByDefault(string text)
        {
            return Context.Event.Where(e => e.NameFull.Contains(text));
        }

        public IEnumerable<Event> SearchByDefaultByGroup(int eventGroupId, string text)
        {
            return Context.Event.Where(e => e.eventGroupId == eventGroupId && e.NameFull.Contains(text));
        }
    }
}

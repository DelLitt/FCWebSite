namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class EventBll : IEventBll
    {
        private IEventDal dalEvent;
        private IEventDal DalEvent
        {
            get
            {
                if (dalEvent == null)
                {
                    dalEvent = DALFactory.Create<IEventDal>();
                }

                return dalEvent;
            }
        }

        public Event GetEvent(int id)
        {
            return DalEvent.GetEvent(id);
        }

        public IEnumerable<Event> GetEvents(IEnumerable<int> ids)
        {
            return DalEvent.GetEvents(ids);
        }

        public IEnumerable<Event> GetAll()
        {
            return DalEvent.GetAll();
        }

        public IEnumerable<Event> GetAllByGroup(int eventGroupId)
        {
            return DalEvent.GetAllByGroup(eventGroupId);
        }

        public IEnumerable<Event> SearchByDefault(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Event[0]; }

            return DalEvent.SearchByDefault(text);
        }

        public IEnumerable<Event> SearchByDefaultByGroup(int eventGroupId, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Event[0]; }

            return DalEvent.SearchByDefaultByGroup(eventGroupId, text);
        }
    }
}

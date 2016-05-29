namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class EventGroupBll : IEventGroupBll
    {
        private IEventGroupDal dalEventGroup;
        private IEventGroupDal DalEventGroup
        {
            get
            {
                if (dalEventGroup == null)
                {
                    dalEventGroup = DALFactory.Create<IEventGroupDal>();
                }

                return dalEventGroup;
            }
        }

        public EventGroup GetEventGroup(int id)
        {
            return DalEventGroup.GetEventGroup(id);
        }

        public IEnumerable<EventGroup> GetEventGroups(IEnumerable<int> ids)
        {
            return DalEventGroup.GetEventGroups(ids);
        }

        public IEnumerable<EventGroup> GetAll()
        {
            return DalEventGroup.GetAll();
        }

        public IEnumerable<EventGroup> SearchByDefault(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new EventGroup[0]; }

            return DalEventGroup.SearchByDefault(text);
        }
    }
}

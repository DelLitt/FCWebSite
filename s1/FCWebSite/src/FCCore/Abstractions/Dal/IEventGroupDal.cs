namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IEventGroupDal : IDalBase
    {
        EventGroup GetEventGroup(int id);
        IEnumerable<EventGroup> GetEventGroups(IEnumerable<int> ids);
        IEnumerable<EventGroup> GetAll();
        IEnumerable<EventGroup> SearchByDefault(string text);
    }
}

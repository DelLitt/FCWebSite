namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IEventGroupBll
    {
        EventGroup GetEventGroup(int id);
        IEnumerable<EventGroup> GetEventGroups(IEnumerable<int> ids);
        IEnumerable<EventGroup> GetAll();
        IEnumerable<EventGroup> SearchByDefault(string text);
    }
}

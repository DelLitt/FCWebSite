namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IEventDal : IDalBase
    {
        Event GetEvent(int id);
        IEnumerable<Event> GetEvents(IEnumerable<int> ids);
        IEnumerable<Event> GetAll();
        IEnumerable<Event> GetAllByGroup(int eventGroupId);
        IEnumerable<Event> SearchByDefault(string text);
        IEnumerable<Event> SearchByDefaultByGroup(int eventGroupId, string text);        
    }
}

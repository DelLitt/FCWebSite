namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IEventBll
    {
        Event GetEvent(int id);
        IEnumerable<Event> GetAll();
        IEnumerable<Event> GetAllByGroup(int eventGroupId);
        IEnumerable<Event> SearchByDefault(string text);
        IEnumerable<Event> SearchByDefaultByGroup(int eventGroupId, string text);
    }
}

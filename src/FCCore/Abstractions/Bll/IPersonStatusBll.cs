namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonStatusBll
    {
        PersonStatus GetPersonStatus(int id);
        IEnumerable<PersonStatus> GetAll();
        IEnumerable<PersonStatus> SearchByNameFull(string text);
    }
}

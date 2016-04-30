namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonStatusDal : IDalBase
    {
        PersonStatus GetPersonStatus(int id);
        IEnumerable<PersonStatus> GetAll();
        IEnumerable<PersonStatus> SearchByNameFull(string text);
    }
}

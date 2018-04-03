namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonRoleBll
    {
        PersonRole GetPersonRole(int id);
        IEnumerable<PersonRole> GetAll();
        IEnumerable<PersonRole> SearchByNameFull(string text);
    }
}

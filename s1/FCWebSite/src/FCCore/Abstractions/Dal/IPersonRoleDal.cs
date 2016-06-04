namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonRoleDal : IDalBase
    {
        PersonRole GetPersonRole(int id);
        IEnumerable<PersonRole> GetPersonRoles(IEnumerable<int> ids);
        IEnumerable<PersonRole> GetAll();
        IEnumerable<PersonRole> SearchByNameFull(string text);
    }
}

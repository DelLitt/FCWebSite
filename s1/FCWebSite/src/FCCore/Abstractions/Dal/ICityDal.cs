namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ICityDal : IDalBase
    {
        City GetCity(int id);
        IEnumerable<City> GetAll();
        IEnumerable<City> SearchByNameFull(string text);
    }
}

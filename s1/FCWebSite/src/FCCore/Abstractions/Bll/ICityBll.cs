namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ICityBll
    {
        City GetCity(int id);
        IEnumerable<City> GetCities(IEnumerable<int> ids);
        IEnumerable<City> GetAll();
        IEnumerable<City> SearchByNameFull(string text);
    }
}

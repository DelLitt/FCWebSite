namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ICityDal : IDalBase
    {
        bool FillCountries { get; set; }

        City GetCity(int id);
        IEnumerable<City> GetCities(IEnumerable<int> ids);
        IEnumerable<City> GetAll();
        IEnumerable<City> SearchByNameFull(string text);
    }
}

namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ICountryBll
    {
        Country GetCountry(int id);
        IEnumerable<Country> GetCountries(IEnumerable<int> ids);
        IEnumerable<Country> GetAll();
        IEnumerable<Country> SearchByNameFull(string text);
    }
}

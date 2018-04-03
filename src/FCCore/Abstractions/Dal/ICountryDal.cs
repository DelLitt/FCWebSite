namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ICountryDal : IDalBase
    {
        Country GetCountry(int id);
        IEnumerable<Country> GetCountries(IEnumerable<int> ids);
        IEnumerable<Country> GetAll();
        IEnumerable<Country> SearchByNameFull(string text);
    }
}

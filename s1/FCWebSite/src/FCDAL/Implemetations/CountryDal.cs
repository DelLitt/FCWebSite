namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public class CountryDal : DalBase, ICountryDal
    {
        public Country GetCountry(int id)
        {
            return Context.Country.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Country> GetCountries(IEnumerable<int> ids)
        {
            if (ids == null) { return new Country[0]; }

            return Context.Country.Where(r => ids.Contains(r.Id));
        }

        public IEnumerable<Country> GetAll()
        {
            return Context.Country;
        }

        public IEnumerable<Country> SearchByNameFull(string text)
        {
            return Context.Country.Where(v => v.NameFull.Contains(text));
        }
    }
}

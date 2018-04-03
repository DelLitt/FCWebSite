namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class CountryBll : ICountryBll
    {
        private ICountryDal dalCountry;
        private ICountryDal DalCountry
        {
            get
            {
                if (dalCountry == null)
                {
                    dalCountry = DALFactory.Create<ICountryDal>();
                }

                return dalCountry;
            }
        }

        public Country GetCountry(int id)
        {
            return DalCountry.GetCountry(id);
        }

        public IEnumerable<Country> GetCountries(IEnumerable<int> ids)
        {
            return DalCountry.GetCountries(ids);
        }

        public IEnumerable<Country> GetAll()
        {
            return DalCountry.GetAll();
        }

        public IEnumerable<Country> SearchByNameFull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Country[0]; }

            return DalCountry.SearchByNameFull(text);
        }
    }
}

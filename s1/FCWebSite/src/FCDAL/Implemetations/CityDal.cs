namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using Exceptions;

    public class CityDal : DalBase, ICityDal
    {
        public bool FillCountries { get; set; } = false;

        public City GetCity(int id)
        {
            City city = Context.City.FirstOrDefault(p => p.Id == id);

            if (city == null) { return null; }

            FillRelations(new City[] { city });

            return city;
        }

        public IEnumerable<City> GetCities(IEnumerable<int> ids)
        {
            if (ids == null) { return new City[0]; }

            IEnumerable<City> cities = Context.City.Where(r => ids.Contains(r.Id));

            FillRelations(cities);

            return cities;
        }

        public IEnumerable<City> GetAll()
        {
            IEnumerable<City> cities = Context.City;

            FillRelations(cities);

            return cities;
        }

        public IEnumerable<City> SearchByNameFull(string text)
        {
            IEnumerable<City> cities = Context.City.Where(v => v.NameFull.Contains(text));

            FillRelations(cities);

            return cities;
        }

        private void FillRelations(IEnumerable<City> cities)
        {
            IEnumerable<Country> countries = new Country[0];

            if (FillCountries)
            {
                var countriesDal = new CountryDal();
                countriesDal.SetContext(Context);

                var citiesIds = new List<int>();
                citiesIds.AddRange(cities.Select(c => (int)c.countryId).Distinct());

                countries = countriesDal.GetCountries(citiesIds).ToList();
            }

            if (countries.Any())
            {
                foreach (City city in cities)
                {
                    if (FillCountries && countries.Any())
                    {
                        city.country = countries.FirstOrDefault(c => c.Id == city.countryId);

                        if (city.country == null)
                        {
                            throw new DalMappingException(nameof(city.country), typeof(City));
                        }
                    }
                }
            }
        }
    }
}

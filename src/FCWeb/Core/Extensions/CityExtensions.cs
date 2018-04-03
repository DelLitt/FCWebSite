namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class CityExtensions
    {
        public static CityViewModel ToViewModel(this City city)
        {
            if (city == null) { return null; }

            return new CityViewModel()
            {
                id = city.Id,
                name = city.Name,
                nameFull = city.NameFull,
                countryId = city.countryId,
                country = city.country.ToViewModel()
            };
        }

        public static IEnumerable<CityViewModel> ToViewModel(this IEnumerable<City> cities)
        {
            if (Guard.IsEmptyIEnumerable(cities)) { return new CityViewModel[0]; }

            return cities.Select(v => v.ToViewModel()).ToList();
        }
    }
}

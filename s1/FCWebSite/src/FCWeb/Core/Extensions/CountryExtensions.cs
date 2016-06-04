namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class CountryExtensions
    {
        public static CountryViewModel ToViewModel(this Country country)
        {
            if (country == null) { return null; }

            return new CountryViewModel()
            {
                id = country.Id,
                name = country.Name,
                nameFull = country.NameFull
            };
        }

        public static IEnumerable<CountryViewModel> ToViewModel(this IEnumerable<Country> countries)
        {
            if (Guard.IsEmptyIEnumerable(countries)) { return new CountryViewModel[0]; }

            return countries.Select(v => v.ToViewModel()).ToList();
        }
    }
}

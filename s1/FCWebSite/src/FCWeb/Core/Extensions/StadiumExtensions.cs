namespace FCWeb.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class StadiumExtensions
    {
        public static StadiumViewModel ToViewModel(this Stadium stadium)
        {
            if (stadium == null) { return null; }

            Guid? tempGuid = stadium.Id == 0 ? Guid.NewGuid() : (Guid?)null;

            return new StadiumViewModel()
            {
                address = stadium.Address,
                capacity = stadium.Capacity,
                cityId = stadium.cityId,
                description = stadium.Description,
                id = stadium.Id,
                image = stadium.Image,
                name = stadium.Name,
                nameFull = stadium.NameFull,
                nameAndCity = stadium.NameExtended(),
                tempGuid = tempGuid
            };
        }


        public static string NameExtended(this Stadium stadium)
        {
            if(stadium == null) { return string.Empty; }

            return stadium.city != null
                    ? string.Format(CultureInfo.CurrentCulture, "{0} ({1})", stadium.Name, stadium.city.NameFull)
                    : stadium.NameFull;
        }

        public static IEnumerable<StadiumViewModel> ToViewModel(this IEnumerable<Stadium> stadiums)
        {
            if (Guard.IsEmptyIEnumerable(stadiums)) { return new StadiumViewModel[0]; }

            return stadiums.Select(v => v.ToViewModel()).ToList();
        }

        public static Stadium ToBaseModel(this StadiumViewModel stadiumViewModel)
        {
            if (stadiumViewModel == null) { return null; }

            return new Stadium()
            {
                Address = stadiumViewModel.address,
                Capacity = stadiumViewModel.capacity,
                cityId = stadiumViewModel.cityId,
                Description = stadiumViewModel.description,
                Id = stadiumViewModel.id,
                Image = stadiumViewModel.image,
                Name = stadiumViewModel.name,
                NameFull = stadiumViewModel.nameFull
            };
        }
    }
}

namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class TourneyExtensions
    {
        public static TourneyViewModel ToViewModel(this Tourney tourney)
        {
            if (tourney == null) { return null; }

            return new TourneyViewModel()
            {
                cityId = tourney.cityId,
                dateEnd = tourney.DateEnd,
                dateStart = tourney.DateStart,
                description = tourney.Description,
                id = tourney.Id,
                name = tourney.Name,
                nameFull = tourney.NameFull,
                tourneyTypeId = tourney.tourneyTypeId
            };
        }

        public static IEnumerable<TourneyViewModel> ToViewModel(this IEnumerable<Tourney> tourneys)
        {
            if (Guard.IsEmptyIEnumerable(tourneys)) { return new TourneyViewModel[0]; }

            return tourneys.Select(v => v.ToViewModel()).ToList();
        }
    }
}

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
                tourneyTypeId = tourney.tourneyTypeId,
                rounds = tourney.Round.ToViewModel()
            };
        }

        public static IEnumerable<TourneyViewModel> ToViewModel(this IEnumerable<Tourney> tourneys)
        {
            if (Guard.IsEmptyIEnumerable(tourneys)) { return new TourneyViewModel[0]; }

            return tourneys.Select(v => v.ToViewModel()).ToList();
        }

        public static Tourney ToBaseModel(this TourneyViewModel tourneyModel)
        {
            if (tourneyModel == null) { return null; }

            return new Tourney()
            {
                Id = tourneyModel.id,
                cityId = tourneyModel.cityId,
                DateEnd = tourneyModel.dateEnd,
                DateStart = tourneyModel.dateStart,
                Description = tourneyModel.description,
                Name = tourneyModel.name,
                NameFull = tourneyModel.nameFull,
                tourneyTypeId = tourneyModel.tourneyTypeId
            };
        }
    }
}

namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class TourneyTypeExtensions
    {
        public static TourneyTypeViewModel ToViewModel(this TourneyType tourneyType)
        {
            if (tourneyType == null) { return null; }

            return new TourneyTypeViewModel()
            {
                id = tourneyType.Id,
                name = tourneyType.Name,
                nameFull = tourneyType.NameFull
            };
        }

        public static IEnumerable<TourneyTypeViewModel> ToViewModel(this IEnumerable<TourneyType> tourneyTypes)
        {
            if (Guard.IsEmptyIEnumerable(tourneyTypes)) { return new TourneyTypeViewModel[0]; }

            return tourneyTypes.Select(t => t.ToViewModel()).ToList();
        }
    }
}

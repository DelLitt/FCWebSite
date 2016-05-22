namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class RoundExtensions
    {
        public static RoundViewModel ToViewModel(this Round round)
        {
            if (round == null) { return null; }

            return new RoundViewModel()
            {
                id = round.Id,
                name = round.Name,
                nameFull = round.NameFull,
                roundFormatId = round.roundFormatId,
                teamList = round.TeamList,
                tourneyId = round.tourneyId
            };
        }

        public static IEnumerable<RoundViewModel> ToViewModel(this IEnumerable<Round> rounds)
        {
            if (Guard.IsEmptyIEnumerable(rounds)) { return new RoundViewModel[0]; }

            return rounds.Select(v => v.ToViewModel()).ToList();
        }
    }
}

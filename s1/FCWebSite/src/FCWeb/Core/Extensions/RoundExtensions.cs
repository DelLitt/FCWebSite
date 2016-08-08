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
                tourneyId = round.tourneyId,
                games = round.Game.ToViewModel()
            };
        }

        public static IEnumerable<RoundViewModel> ToViewModel(this IEnumerable<Round> rounds)
        {
            if (Guard.IsEmptyIEnumerable(rounds)) { return new RoundViewModel[0]; }

            return rounds.Select(v => v.ToViewModel()).ToList();
        }

        public static Round ToBaseModel(this RoundViewModel roundModel)
        {
            if (roundModel == null) { return null; }

            if(roundModel.editMode)
            {
                roundModel.teamList = !Guard.IsEmptyIEnumerable(roundModel.teams)
                    ? "[" + string.Join(",", roundModel.teams.Select(t => t.id)) + "]"
                    : "[]";
            }

            return new Round()
            {
                Id = roundModel.id,
                Name = roundModel.name,
                NameFull = roundModel.nameFull,
                roundFormatId = roundModel.roundFormatId,
                TeamList = roundModel.teamList,
                tourneyId = roundModel.tourneyId
            };
        }
    }
}

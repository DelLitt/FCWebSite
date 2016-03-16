using FCCore.Model;
using FCWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCWeb.Core.Extensions
{
    public static class ViewModelExtensions
    {
        public static IEnumerable<PublicationViewModel> ToViewModel(this IEnumerable<Publication> publications)
        {
            if(publications == null || !publications.Any())
            {
                return new PublicationViewModel[0];
            }

            return publications.Select(p => new PublicationViewModel()
            {
                title = p.Title,
                img = "http://sfc-slutsk.by/" + p.Image
            });
        }

        public static RankingTableViewModel ToViewModel(this IEnumerable<TableRecord> tableRecords, string tourneyName)
        {
            IEnumerable<TableRecordViewModel> rows = tableRecords == null
                ? new TableRecordViewModel[0]
                : tableRecords.Select(p => new TableRecordViewModel()
                    {
                        draws = p.Draws,
                        games = p.Games,
                        goalsAgainst = p.GoalsAgainst,
                        goalsFor = p.GoalsFor,
                        loses = p.Loses,
                        points = p.Points,
                        position = p.Position,
                        team = p.Team.Name,
                        wins = p.Wins
                    });


            return new RankingTableViewModel()
            {
                name = tourneyName,
                rows = rows
            };
        }
    }
}

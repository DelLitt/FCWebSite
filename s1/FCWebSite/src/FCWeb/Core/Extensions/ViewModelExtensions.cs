namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using FCWeb.ViewModels;

    public static class ViewModelExtensions
    {
        public static IEnumerable<PublicationShortViewModel> ToShortViewModel(this IEnumerable<Publication> publications)
        {
            if(Guard.IsEmptyIEnumerable(publications)) { return new PublicationShortViewModel[0]; }

            return publications.Select(p => new PublicationShortViewModel()
            {
                id = p.Id,
                title = p.Title,
                img = "http://sfc-slutsk.by/" + p.Image
            });
        }

        public static PublicationViewModel ToViewModel(this Publication publication)
        {
            if (publication == null) { return null; }

            return new PublicationViewModel()
            {
                id = publication.Id,
                articleId = publication.articleId,
                contentHTML = publication.ContentHTML,
                dateChanged = publication.DateChanged,
                dateCreated = publication.DateCreated,
                dateDisplayed = publication.DateDisplayed,
                enable = publication.Enable,
                header = publication.Header,
                image = "http://sfc-slutsk.by/" + publication.Image,
                imageGalleryId = publication.imageGalleryId,
                priority = publication.Priority,
                showImageInContet = publication.ShowImageInContet,
                title = publication.Title,
                urlKey = publication.URLKey,
                videoId = publication.videoId,
                visibility = publication.Visibility,
                lead = publication.Lead
            };
        }

        public static IEnumerable<PublicationViewModel> ToViewModel(this IEnumerable<Publication> publications)
        {
            if (Guard.IsEmptyIEnumerable(publications)) { return new PublicationViewModel[0]; }

            return publications.Select(p => p.ToViewModel());
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

        public static IEnumerable<RoundViewModel> ToRoundViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new RoundViewModel[0]; }

            IEnumerable<IGrouping<short, Game>> grouppedGamesByRound = games.GroupBy(g => g.roundId);
            var roundViews = new List<RoundViewModel>();

            foreach (IGrouping<short, Game> roundGames in grouppedGamesByRound)
            {
                var firstGame = roundGames.First();
                var roundView = new RoundViewModel()
                {
                    //tourney = firstGame?.round?.tourney?.Name ?? string.Empty,
                    tourney = "Турнир",
                    logo = string.Empty,
                    roundId = firstGame.roundId,
                    name = firstGame?.round?.Name ?? string.Empty,
                    dateGames = new List<DayGamesShortViewModel>()
                };

                IEnumerable<IGrouping<string, Game>> grouppedGamesByDay =
                    roundGames.GroupBy(g => g.GameDate.ToString("MMM d"));

                foreach (var dayGames in grouppedGamesByDay)
                {
                    var dayGameInfo = dayGames.First();
                    roundView.dateGames.Add(new DayGamesShortViewModel()
                    {
                        date = new DateInfoFormat1()
                        {
                            date = dayGameInfo.GameDate,
                            dateString = dayGameInfo.GameDate.ToString("d MMMM"),
                            dayString = dayGameInfo.GameDate.ToString("dddd")
                        },
                        games = dayGames.ToGameShortViewModel()
                    });
                }

                roundViews.Add(roundView);
            }

            return roundViews;
        }

        public static IEnumerable<GameShortViewModel> ToGameShortViewModel(this IEnumerable<Game> games)
        {
            if(Guard.IsEmptyIEnumerable(games)) { return new GameShortViewModel[0]; }

            return games.Select(g => new GameShortViewModel()
            {
                home = g?.home?.Name ?? string.Empty,
                away = g?.away?.Name ?? string.Empty,
                homeScore = g.homeScore,
                awayScore = g.awayScore,
                extra = g.HomePenalties.HasValue ? "пен." : (g.HomeAddScore.HasValue ? "доп." : string.Empty),
                start = g.GameDate,
                time = g.GameDate.ToString("HH:mm")
            });
        }
    }
}

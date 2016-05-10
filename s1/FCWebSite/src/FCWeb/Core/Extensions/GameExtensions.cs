namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class GameExtensions
    {
        public static GameViewModel ToViewModel(this Game game)
        {
            if (game == null) { return null; }

            return new GameViewModel()
            {
                audience = game.Audience,
                awayAddScore = game.AwayAddScore,
                awayId = game.awayId,
                awayPenalties = game.AwayPenalties,
                awayScore = game.awayScore,
                gameDate = game.GameDate,
                homeAddScore = game.HomeAddScore,
                homeId = game.homeId,
                homePenalties = game.HomePenalties,
                homeScore = game.homeScore,
                id = game.Id,
                imageGalleryId = game.imageGalleryId,
                note = game.Note,
                played = game.Played,
                referees = game.Referees,
                roundId = game.roundId,
                showTime = game.ShowTime,
                stadiumId = game.stadiumId,
                videoId = game.videoId
            };
        }

        public static IEnumerable<GameViewModel> ToViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new GameViewModel[0]; }

            return games.Select(v => v.ToViewModel()).ToList();
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
            if (Guard.IsEmptyIEnumerable(games)) { return new GameShortViewModel[0]; }

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

namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;
    using ViewModels.Game;

    public static class GameExtensions
    {
        public static GameViewModel ToViewModel(this Game game)
        {
            if (game == null) { return null; }

            // To avoid infinite recursion
            if(game.round != null) { game.round.Game = null; }

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
                videoId = game.videoId,
                round = game.round.ToViewModel(),
                home = game.home.ToViewModel(),
                away = game.away.ToViewModel()
            };
        }

        public static IEnumerable<GameViewModel> ToViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new GameViewModel[0]; }

            return games.Select(v => v.ToViewModel()).ToList();
        }

        public static Game ToBaseModel(this GameViewModel gameView)
        {
            if (gameView == null) { return null; }

            return new Game()
            {
                Audience = gameView.audience,
                AwayAddScore = gameView.awayAddScore,
                awayId = gameView.awayId,
                AwayPenalties = gameView.awayPenalties,
                awayScore = gameView.awayScore,
                GameDate = gameView.gameDate,
                HomeAddScore = gameView.homeAddScore,
                homeId = gameView.homeId,
                HomePenalties = gameView.homePenalties,
                Id = gameView.id,
                homeScore = gameView.homeScore,
                imageGalleryId = gameView.imageGalleryId,
                Note = gameView.note,
                Played = gameView.played,
                roundId = gameView.roundId,
                Referees = gameView.referees,
                ShowTime = gameView.showTime,
                stadiumId = gameView.stadiumId,
                videoId = gameView.videoId
            };
        }

        public static IEnumerable<RoundInfoViewModel> ToRoundInfoViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new RoundInfoViewModel[0]; }

            IEnumerable<IGrouping<short, Game>> grouppedGamesByRound = games.GroupBy(g => g.roundId);
            var roundViews = new List<RoundInfoViewModel>();

            foreach (IGrouping<short, Game> roundGames in grouppedGamesByRound)
            {
                var firstGame = roundGames.First();
                var roundView = new RoundInfoViewModel()
                {
                    tourney = firstGame?.round?.tourney?.Name ?? "Турнир",
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

        public static GameShortViewModel ToGameShortViewModel(this Game game)
        {
            return new GameShortViewModel()
            {
                id = game.Id,
                home = new EntityLinkViewModel()
                {
                    id = game.home.Id.ToString(),
                    image = game.home.Image,
                    text = game.home.Name,
                    title = game.home.Name
                },
                away = new EntityLinkViewModel()
                {
                    id = game.away.Id.ToString(),
                    image = game.away.Image,
                    text = game.away.Name,
                    title = game.away.Name
                },
                homeScore = game.homeScore,
                awayScore = game.awayScore,
                extra = game.HomePenalties.HasValue ? "2" : (game.HomeAddScore.HasValue ? "1" : string.Empty),
                start = game.GameDate,
                time = game.GameDate.ToString("HH:mm"),
                showTime = game.ShowTime
            };
        }

        public static IEnumerable<GameShortViewModel> ToGameShortViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new GameShortViewModel[0]; }
            return games.Select(g => g.ToGameShortViewModel());
        }

        public static GameQuickInfoViewModel ToGameQuickInfoViewModel(this Game game)
        {
            return new GameQuickInfoViewModel()
            {
                id = game.Id,
                home = new EntityLinkViewModel()
                {
                    id = game.home.Id.ToString(),
                    image = game.home.Image,
                    text = game.home.Name,
                    title = game.home.Name
                },
                away = new EntityLinkViewModel()
                {
                    id = game.away.Id.ToString(),
                    image = game.away.Image,
                    text = game.away.Name,
                    title = game.away.Name
                },
                homeScore = game.homeScore,
                awayScore = game.awayScore,
                extra = game.HomePenalties.HasValue ? "2" : (game.HomeAddScore.HasValue ? "1" : string.Empty),
                start = game.GameDate,
                time = game.GameDate.ToString("HH:mm"),
                round = game?.round?.NameFull ?? string.Empty,
                showTime = game.ShowTime,
                stadium = game?.stadium.NameExtended() ?? string.Empty,
                tourney = game?.round?.tourney?.NameFull ?? string.Empty
            };
        }

        public static IEnumerable<GameQuickInfoViewModel> ToGameQuickInfoViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new GameQuickInfoViewModel[0]; }

            return games.Select(g => g.ToGameQuickInfoViewModel());
        }
    }
}

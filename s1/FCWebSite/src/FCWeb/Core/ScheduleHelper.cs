namespace FCWeb.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels.Schedule;
    using FCCore.Model;
    using ViewModels;

    public class ScheduleHelper
    {
        public static IEnumerable<ScheduleItemViewModel> GetTourneysShcedule(IEnumerable<int> tourneyIds)
        {         
            ILogger<ScheduleHelper> logger = MainCfg.ServiceProvider.GetService<ILogger<ScheduleHelper>>();
            logger.LogTrace("Getting schedule. Tournaments count: {0}.", tourneyIds.Count());

            IList<ScheduleItemViewModel> schedule = new List<ScheduleItemViewModel>();

            ITourneyBll tourneyBll = MainCfg.ServiceProvider.GetService<ITourneyBll>();
            IEnumerable<Tourney> tourneys = tourneyBll.GetTourneys(tourneyIds);
            if (!tourneyIds.Any()) { return schedule; }

            logger.LogTrace("Tournaments ids: {0}.", string.Join(", ", tourneyIds));

            IRoundBll roundBll = MainCfg.ServiceProvider.GetService<IRoundBll>();
            IEnumerable<Round> rounds = roundBll.GetRoundsOfTourneys(tourneys.Select(t => (int)t.Id));
            if (!rounds.Any()) { return schedule; }

            IGameBll gameBll = MainCfg.ServiceProvider.GetService<IGameBll>();
            IEnumerable<Game> games = gameBll.GetGamesByRounds(rounds.Select(r => (int)r.Id)).OrderByDescending(d => d.GameDate);
            if (!games.Any()) { return schedule; }

            var allTeamIds = new List<int>();
            allTeamIds.AddRange(games.Select(g => g.homeId));
            allTeamIds.AddRange(games.Select(g => g.awayId));

            ITeamBll teamBll = MainCfg.ServiceProvider.GetService<ITeamBll>();
            IEnumerable<Team> teams = teamBll.GetTeams(allTeamIds.Distinct());
            if (!teams.Any()) { return schedule; }

            int roundId = 0;
            Round round = null;
            Tourney tourney = null;
            IList<ScheduleGameViewModel> gameGroups = new List<ScheduleGameViewModel>();

            foreach (Game game in games)
            {
                if (roundId != game.roundId && gameGroups.Any())
                {
                    // Will be guaranteed
                    round = rounds.First(r => r.Id == game.roundId);
                    tourney = tourneys.First(t => t.Id == round.tourneyId);

                    var dayGamseViews = new List<DayGamesViewModel>();

                    IEnumerable<IGrouping<DayOfWeek, ScheduleGameViewModel>> grouppedGamesByDay =
                        gameGroups.GroupBy(g => g.date.DayOfWeek);

                    foreach (var dayGames in grouppedGamesByDay)
                    {
                        var dayGameInfo = dayGames.First();
                        dayGamseViews.Add(new DayGamesViewModel()
                        {
                            day = dayGames.Key.ToString().ToUpper(),
                            games = dayGames
                        });
                    }

                    schedule.Add(new ScheduleItemViewModel()
                    {
                        date = game.GameDate,
                        round = new EntityLinkViewModel()
                        {
                            id = round.Id.ToString(),
                            text = round.Name,
                            title = round.NameFull
                        },
                        tourney = new EntityLinkViewModel()
                        {
                            id = tourney.Id.ToString(),
                            text = tourney.Name,
                            title = tourney.NameFull
                        },
                        daysGames = dayGamseViews
                    });

                    gameGroups = new List<ScheduleGameViewModel>();
                }

                roundId = game.roundId;

                Team home = teams.FirstOrDefault(t => t.Id == game.homeId);
                Team away = teams.FirstOrDefault(t => t.Id == game.awayId);

                if (home == null || away == null)
                {
                    logger.LogWarning("Couldn't get game (Id: {0}) for scheduler of tourney (Id: {1}). Home (Id: {2}) is {3}found. Away (Id: {4}) is {5}found.",
                        game.Id,
                        tourney.Id,
                        game.homeId,
                        home == null ? "NOT " : string.Empty,
                        game.awayId,
                        away == null ? "NOT " : string.Empty);

                    continue;
                }

                gameGroups.Add(new ScheduleGameViewModel()
                {
                    id = game.Id,
                    away = new EntityLinkViewModel()
                    {
                        id = game.awayId.ToString(),
                        text = away.Name,
                        title = away.Name,
                        image = away.Image
                    },
                    awayAddScore = game.AwayAddScore,
                    awayPenalties = game.AwayPenalties,
                    awayScore = game.awayScore,
                    date = game.GameDate,
                    home = new EntityLinkViewModel()
                    {
                        id = game.homeId.ToString(),
                        text = home.Name,
                        title = home.Name,
                        image = home.Image
                    },
                    homeAddScore = game.HomeAddScore,
                    homePenalties = game.HomePenalties,
                    homeScore = game.homeScore,
                    roundId = game.roundId,
                    showTime = game.ShowTime,
                    played = game.Played
                });
            }

            if (round != null && gameGroups.Any())
            {
                ScheduleGameViewModel game = gameGroups.Last();

                var dayGamseViews = new List<DayGamesViewModel>();

                IEnumerable<IGrouping<DayOfWeek, ScheduleGameViewModel>> grouppedGamesByDay =
                    gameGroups.GroupBy(g => g.date.DayOfWeek);

                foreach (var dayGames in grouppedGamesByDay)
                {
                    var dayGameInfo = dayGames.First();
                    dayGamseViews.Add(new DayGamesViewModel()
                    {
                        day = dayGames.Key.ToString().ToUpper(),
                        games = dayGames
                    });
                }

                schedule.Add(new ScheduleItemViewModel()
                {
                    date = game.date,
                    round = new EntityLinkViewModel()
                    {
                        id = round.Id.ToString(),
                        text = round.Name,
                        title = round.NameFull
                    },
                    daysGames = dayGamseViews
                });
            }

            return schedule;
        }
    }
}

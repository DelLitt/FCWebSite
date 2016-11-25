namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using Microsoft.AspNet.Mvc;
    using ViewModels;
    using ViewModels.Schedule;

    [Route("api/games/[controller]")]
    public class ScheduleController : Controller
    {
        //[FromServices]
        private ITourneyBll tourneyBll { get; set; }
        private IRoundBll roundBll { get; set; }
        private IGameBll gameBll { get; set; }
        private ITeamBll teamBll { get; set; }

        public ScheduleController(ITourneyBll tourneyBll, IRoundBll roundBll, IGameBll gameBll, ITeamBll teamBll)
        {
            this.tourneyBll = tourneyBll;
            this.roundBll = roundBll;
            this.gameBll = gameBll;
            this.teamBll = teamBll;
        }

        [HttpGet]
        public IEnumerable<ScheduleItemViewModel> Get([FromQuery] int[] tourneyIds)
        {
            IList<ScheduleItemViewModel> schedule = new List<ScheduleItemViewModel>();

            IEnumerable<Tourney> tourneys = tourneyBll.GetTourneys(tourneyIds);

            if(!tourneyIds.Any()) { return schedule;  }

            IEnumerable<Round> rounds = roundBll.GetRoundsOfTourneys(tourneys.Select(t => (int)t.Id));

            if (!rounds.Any()) { return schedule; }

            IEnumerable<Game> games = gameBll.GetGamesByRounds(rounds.Select(r => (int)r.Id)).OrderByDescending(d => d.GameDate);

            if (!games.Any()) { return schedule; }

            var allTeamIds = new List<int>();

            allTeamIds.AddRange(games.Select(g => g.homeId));
            allTeamIds.AddRange(games.Select(g => g.awayId));

            IEnumerable<Team> teams = teamBll.GetTeams(allTeamIds.Distinct());

            if (!teams.Any()) { return schedule; }

            int roundId = 0;
            Round round = null;
            Tourney tourney = null;
            IList<ScheduleGameViewModel> gameGroups = new List<ScheduleGameViewModel>();

            foreach(Game game in games)
            {
                if(roundId != game.roundId && gameGroups.Any())
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

                // Will be guaranteed
                Team home = teams.First(t => t.Id == game.homeId);
                Team away = teams.First(t => t.Id == game.awayId);

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

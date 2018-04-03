namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITourneyBll: IFCBll
    {
        bool FillRounds { get; set; }
        bool FillGames { get; set; }

        System.DateTime Test(string key, System.DateTime value, int seconds);
        Tourney GetTourney(int tourneyId);
        Tourney GetTourneyByRoundId(int roundId);
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
        IEnumerable<Tourney> GetAll();
        IEnumerable<Tourney> SearchByNameFull(string text);
        Tourney SaveTourney(Tourney entity);
    }
}

namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITourneyBll
    {
        Tourney GetTourney(int tourneyId);
        IEnumerable<Tourney> GetAll();
        IEnumerable<Tourney> SearchByNameFull(string text);
    }
}

namespace FCCore.Abstractions.Bll
{
    using Model;

    public interface ITourneyBll
    {
        Tourney GetTourney(int tourneyId);
    }
}

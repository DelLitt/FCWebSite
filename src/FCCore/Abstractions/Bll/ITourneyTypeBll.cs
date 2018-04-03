namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITourneyTypeBll : IFCBll
    {
        TourneyType GetTourneyType(int tourneyTypeId);
        IEnumerable<TourneyType> GetAll();
        IEnumerable<TourneyType> SearchByDefault(string text);
    }
}

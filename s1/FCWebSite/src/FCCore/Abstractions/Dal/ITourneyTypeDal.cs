namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ITourneyTypeDal : IDalBase
    {
        TourneyType GetTourneyType(int tourneyTypeId);
        IEnumerable<TourneyType> GetAll();
        IEnumerable<TourneyType> SearchByDefault(string text);
    }
}

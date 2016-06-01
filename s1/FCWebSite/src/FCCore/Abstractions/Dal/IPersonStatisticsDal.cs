namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonStatisticsDal : IDalBase
    {
        PersonStatistics GetPersonStatistics(int personId);
        IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId);
        int SavePersonStatistics(PersonStatistics entity);
    }
}

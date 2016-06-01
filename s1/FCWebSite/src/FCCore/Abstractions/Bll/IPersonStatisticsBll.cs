namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonStatisticsBll
    {
        PersonStatistics GetPersonStatistics(int personId);
        IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId);
        int SavePersonStatistics(PersonStatistics entity);
    }
}

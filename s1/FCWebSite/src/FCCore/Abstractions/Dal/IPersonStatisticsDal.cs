namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonStatisticsDal : IDalBase
    {
        bool FillTeams { get; set; }
        bool FillTourneys { get; set; }

        IEnumerable<PersonStatistics> GetPersonStatistics(int personId);
        IEnumerable<PersonStatistics> GetPersonsStatistics(int temaId, int tourneyId);
        IEnumerable<PersonStatistics> GetPersonsStatistics(int tourneyId, IEnumerable<int> personIds);
        int SavePersonStatistics(int tourneyId, IEnumerable<PersonStatistics> personStatistics);
    }
}

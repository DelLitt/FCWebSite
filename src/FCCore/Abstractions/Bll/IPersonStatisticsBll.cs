namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IPersonStatisticsBll : IFCBll
    {
        bool FillTeams { get; set; }
        bool FillTourneys { get; set; }

        IEnumerable<PersonStatistics> GetPersonStatistics(int personId);
        IEnumerable<PersonStatistics> GetPersonsStatistics(int teamId, int tourneyId);
        IEnumerable<PersonStatistics> CalculateTourneyStatistics(int tourneyId, IEnumerable<Person> persons);
        int SavePersonStatistics(int tourneyId, IEnumerable<PersonStatistics> personStatistics);
    }
}

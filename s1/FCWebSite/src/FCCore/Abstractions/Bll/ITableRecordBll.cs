using FCCore.Model;
using System.Collections.Generic;

namespace FCCore.Abstractions.Bll
{
    public interface ITableRecordBll
    {
        bool FillTeams { get; set; }
        bool FillTourney { get; set; }

        IEnumerable<TableRecord> GetTourneyTable(int tourneyId);
        void SaveTourneyTable(int tourneyId, IEnumerable<TableRecord> tableRecords);
    }
}

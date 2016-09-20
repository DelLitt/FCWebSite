using FCCore.Common;
using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.Dal
{
    public interface ITableRecordDal : IDalBase
    {
        bool FillTeams { get; set; }
        bool FillTourney { get; set; }

        IEnumerable<TableRecord> GetTourneyTable(int tourneyId);
        void SaveTourneyTable(int tourneyId, IEnumerable<TableRecord> tableRecords);
    }
}

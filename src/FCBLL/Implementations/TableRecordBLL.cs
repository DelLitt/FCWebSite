namespace FCBLL.Implementations
{
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using System.Collections.Generic;
    using FCCore.Model;
    using System;
    using System.Linq;
    using FCCore.Common;
    public class TableRecordBll : ITableRecordBll
    {
         public bool FillTeams
        {
            get
            {
                return DalTableRecord.FillTeams;
            }

            set
            {
                DalTableRecord.FillTeams = value;
            }
        }

        public bool FillTourney
        {
            get
            {
                return DalTableRecord.FillTourney;
            }

            set
            {
                DalTableRecord.FillTourney = value;
            }
        }

        private ITableRecordDal dalTableRecord;
        private ITableRecordDal DalTableRecord
        {
            get
            {
                if (dalTableRecord == null)
                {
                    dalTableRecord = DALFactory.Create<ITableRecordDal>();
                }

                return dalTableRecord;
            }
        }

        public IEnumerable<TableRecord> GetTourneyTable(int tourneyId)
        {
            return DalTableRecord.GetTourneyTable(tourneyId).OrderBy(tr => tr.Position);
        }

        public void SaveTourneyTable(int tourneyId, IEnumerable<TableRecord> tableRecords)
        {
            DalTableRecord.SaveTourneyTable(tourneyId, tableRecords);
        }
    }
}

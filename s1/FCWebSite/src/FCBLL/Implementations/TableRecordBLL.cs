using FCCore.Abstractions.Bll;
using FCCore.Abstractions.Dal;
using System.Collections.Generic;
using FCCore.Model;
using System;

namespace FCBLL.Implementations
{
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
            return DalTableRecord.GetTourneyTable(tourneyId);
        }
    }
}

namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;
    using Microsoft.Data.Entity.ChangeTracking;
    public class TableRecordDal : DalBase, ITableRecordDal
    {
        public bool FillTeams { get; set; } = false;
        public bool FillTourney { get; set; } = false;

        public IEnumerable<TableRecord> GetTourneyTable(int tourneyId)
        {
            IEnumerable<TableRecord> records = Context.TableRecord.Where(tr => tr.tourneyId == tourneyId).ToList();

            FillRelations(records);

            return records;
        }

        public int SaveTourneyTable(int tourneyId, IEnumerable<TableRecord> tableRecords)
        {
            if (tableRecords == null) { return -1; }

            List<TableRecord> saveRecords = tableRecords.Where(r => r.tourneyId == tourneyId).ToList();
            List<TableRecord> dbRecords = GetTourneyTable(tourneyId).ToList();
            var insertRecords = new List<TableRecord>();
            IEnumerable<TableRecord> removeRecords = new TableRecord[] { };

            int removeCount = dbRecords.Count - saveRecords.Count;
            if (removeCount > 0)
            {
                removeRecords = dbRecords.Skip(saveRecords.Count);
                Context.RemoveRange(removeRecords);
            }

            for(int i = 0; i < saveRecords.Count; i++)
            {
                TableRecord dbRecord = dbRecords.ElementAtOrDefault(i);

                if (dbRecord == null)
                {
                    dbRecord = new TableRecord();
                    insertRecords.Add(dbRecord);
                }

                dbRecord.Active = saveRecords[i].Active;
                dbRecord.Draws = saveRecords[i].Draws;
                dbRecord.Games = saveRecords[i].Games;
                dbRecord.GoalsAgainst = saveRecords[i].GoalsAgainst;
                dbRecord.GoalsFor = saveRecords[i].GoalsFor;
                dbRecord.Loses = saveRecords[i].Loses;
                dbRecord.Points = saveRecords[i].Points;
                dbRecord.PointsVirtual = saveRecords[i].PointsVirtual;
                dbRecord.Position = saveRecords[i].Position;
                dbRecord.teamId = saveRecords[i].teamId;
                dbRecord.Wins = saveRecords[i].Wins;
                dbRecord.tourneyId = (short)tourneyId;
            }

            Context.TableRecord.AddRange(insertRecords, Microsoft.Data.Entity.GraphBehavior.SingleObject);

            try
            {
                return Context.SaveChanges();
            }
            catch(Exception ex)
            {
                //TODO: Add logging
                throw;                
            }
        }

        private void FillRelations(IEnumerable<TableRecord> tableRecords)
        {
            if(Guard.IsEmptyIEnumerable(tableRecords)) { return; }

            Tourney tourney = null;
            IEnumerable<Team> teams = new Team[0];

            if (FillTeams)
            {
                var teamDal = new TeamDal();
                teamDal.SetContext(Context);

                teams = teamDal.GetTeams(tableRecords.Select(r => r.teamId));
            }

            if (FillTourney)
            {
                var tourneyDal = new TourneyDal();
                tourneyDal.SetContext(Context);

                tourney = tourneyDal.GetTourney(tableRecords.First().tourneyId);

                if (tourney == null)
                {
                    throw new DalMappingException(nameof(tourney), typeof(TableRecord));
                }
            }

            if (teams.Any() || tourney != null)
            {
                foreach (TableRecord record in tableRecords)
                {
                    if (FillTeams && teams.Any())
                    {
                        record.Team = teams.FirstOrDefault(t => t.Id == record.teamId);

                        if (record.Team == null)
                        {
                            throw new DalMappingException(nameof(record.Team), typeof(TableRecord));
                        }
                    }

                    record.tourney = FillTourney ? tourney : null;
                }
            }
        }
    }
}

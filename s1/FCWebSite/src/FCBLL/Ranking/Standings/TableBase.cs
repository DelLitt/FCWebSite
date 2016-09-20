namespace FCBLL.Ranking.Standings
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Builders;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Diagnostic.Logging.Simple;
    using FCCore.Diagnostic.Transformation.Extensions;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public abstract class TableBase
    {        
        private object lockLogs = new object();

        protected IAccumulativeLog log = MainCfg.ServiceProvider.GetService<IAccumulativeLog>();

        public TableRecord[] Records { get; set; }        

        public virtual string Name { get { return GetType().Name; } }

        public virtual int Rule { get { return 1; } }

        public TableBase()
        {                        
            Records = new TableRecord[] { };
            Init();
        }

        public TableBase(TableRecord[] tableRecords)
        {
            Guard.CheckNull(tableRecords, nameof(tableRecords));
            Records = tableRecords;
            Init();
        }

        private void Init()
        {
            log.Enable = true;

            short position = 1;
            foreach (TableRecord tr in Records)
            {
                tr.Position = position++;
                tr.PointsVirtual = TableRecordBuilder.NotCalculatedValue;
            }

            log.Trace("Table '{0}' initialized.", Name);
        }

        public abstract void Sort();

        protected virtual short PointsForWin { get { return 3; } }
        protected virtual short PointsForDraw { get { return 1; } }

        public virtual void BuildFromGames(short tourneyId, IEnumerable<Game> games)
        {
            Guard.CheckNull(games, nameof(games));

            if (!games.Any())
            {
                log.Trace("There are no games to build the table '{0}' of tourney [Id:{1}]", Name, tourneyId);
                return;
            }

            log.Trace("Building table '{0}' of tourney [Id:{1}] from games is started.", Name, tourneyId);
            log.Trace("Games count is {0}", games.Count());

            IEnumerable<int> teamIds = games.Select(g => g.homeId).Union(games.Select(g => g.awayId)).Distinct();

            log.Trace("{0} different team ids are selected from the games.", teamIds.Count());
            log.Trace("Here are team ids: [{0}]", string.Join(",", teamIds));

            var trBuilder = new TableRecordBuilder(games);

            trBuilder.PointsForWin = PointsForWin;
            trBuilder.PointsForDraw = PointsForDraw;

            log.Trace("Table record builder is created.");
            log.Trace("Points for win is set to {0}.", PointsForWin);
            log.Trace("Points for draw is set to {0}.", PointsForDraw);

            var tableRecords = new List<TableRecord>();

            log.Trace("Creating table records is started");
            foreach (int teamId in teamIds)
            {
                TableRecord tr = trBuilder.GetTableRecord(teamId, tourneyId);
                tableRecords.Add(tr);
            }

            Records = tableRecords.ToArray();

            log.Trace("Initial state of table '{0}' built from games:", Name);
            log.Trace(Records.ToTextTable());
        }

        protected void SaveLog()
        {
            string logDir = Path.Combine(MainCfg.LogPath, "tables");

            if(!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            string logPath = Path.Combine(logDir, "TablesCalculating.log");

            lock (lockLogs)
            {
                File.AppendAllText(logPath, log.Log);
                File.AppendAllLines(logPath, new[] { string.Empty });
            }
        }

        protected static T TempClone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}

namespace FCCore.Abstractions.Bll.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Model;

    public interface IRanking
    {
        IEnumerable<TableRecord> CalculateTable(int tourneyId);
        IEnumerable<TableRecord> CalculateTable(Tourney tourney);
        IEnumerable<TableRecord> CalculateTable(int rule, IEnumerable<Game> games, short tourneyId = 0);
    }
}

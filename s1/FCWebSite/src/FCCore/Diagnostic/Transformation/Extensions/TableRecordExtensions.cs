namespace FCCore.Diagnostic.Transformation.Extensions
{
    using System.Collections.Generic;
    using Common;
    using Model;

    public static class TableRecordExtensions
    {
        public static string ToTextTable(this IEnumerable<TableRecord> tableRecords, bool fromNewLine = true)
        {
            if(Guard.IsEmptyIEnumerable(tableRecords)) { return string.Empty; }

            return Transform.TableRecordsToTextTable(tableRecords, fromNewLine);
        }
    }
}

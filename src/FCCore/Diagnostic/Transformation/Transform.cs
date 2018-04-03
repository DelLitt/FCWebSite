namespace FCCore.Diagnostic.Transformation
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Model;

    public static class Transform
    {
        private const string TableRecordTextRowTemplate = "|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|";

        private static string tableRecordTextHeader;
        public static string TableRecordTextHeader
        {
            get
            {

                if (string.IsNullOrWhiteSpace(tableRecordTextHeader))
                {
                    tableRecordTextHeader = string.Format(
                                                CultureInfo.InvariantCulture,
                                                TableRecordTextRowTemplate,
                                                    "P.", "T.", "G.", "W.", "D.", "L.", "GF", "GA", "P.", "VP");
                }

                return tableRecordTextHeader;
            }
        }

        public static string TableRecordToTextTableRow(TableRecord tableRecord)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                TableRecordTextRowTemplate,
                    TwoDigs(tableRecord.Position),
                    TwoDigs(tableRecord.teamId),
                    TwoDigs(tableRecord.Games),
                    TwoDigs(tableRecord.Wins),
                    TwoDigs(tableRecord.Draws),
                    TwoDigs(tableRecord.Loses),
                    TwoDigs(tableRecord.GoalsFor),
                    TwoDigs(tableRecord.GoalsAgainst),
                    TwoDigs(tableRecord.Points),
                    TwoDigs(tableRecord.PointsVirtual));
        }

        public static string TableRecordsToTextTable(IEnumerable<TableRecord> tableRecords, bool fromNewLine = true)
        {
            var sb = new StringBuilder();

            if(fromNewLine)
            {
                sb.AppendLine(string.Empty);
            }

            sb.AppendLine(TableRecordTextHeader);

            foreach(TableRecord tr in tableRecords)
            {
                sb.AppendLine(TableRecordToTextTableRow(tr));
            }

            return sb.ToString();
        }

        private static string TwoDigs(int value)
        {
            return value > 9 ? value.ToString() : " " + value.ToString();
        }
    }
}

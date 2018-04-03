namespace FCCore.Common.Comparers
{
    using System.Collections.Generic;
    using Model;

    public class TableRecordsStoreComarer : IEqualityComparer<TableRecord>
    {
        public bool Equals(TableRecord x, TableRecord y)
        {
            return x.teamId == y.teamId;
        }

        public int GetHashCode(TableRecord obj)
        {
            return obj.GetHashCode();
        }
    }
}

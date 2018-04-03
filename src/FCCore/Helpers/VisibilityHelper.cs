namespace FCCore.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Utils;

    public class VisibilityHelper
    {
        public static VisibilityGroups TypeFromString(string group)
        {
            return EnumUtils.FromString<VisibilityGroups>(group);
        }

        public static int ValueFromString(string group)
        {
            return Convert.ToInt32(EnumUtils.FromString<VisibilityGroups>(group));
        }

        public static IEnumerable<int> ValuesFromStrings(IEnumerable<string> groups)
        {
            if(Guard.IsEmptyIEnumerable(groups)) { return new int[0]; }
            
            int[] values = new int[groups.Count()];

            for(int i = 0; i < groups.Count(); i++ )
            {
                values[i] = Convert.ToInt32(EnumUtils.FromString<VisibilityGroups>(groups.ElementAt(i)));
            }

            return values;
        }

        public static int VisibilityFromStrings(IEnumerable<string> strings)
        {
            if (Guard.IsEmptyIEnumerable(strings)) { return 0; }

            IEnumerable<int> groupsValues = ValuesFromStrings(strings);

            if (Guard.IsEmptyIEnumerable(groupsValues)) { return 0; }

            int visibility = 0;

            foreach (int gv in groupsValues)
            {
                visibility = visibility | gv;
            }

            return visibility;
        }
    }
}

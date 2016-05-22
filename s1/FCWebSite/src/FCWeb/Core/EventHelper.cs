namespace FCWeb.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Common.Constants;
    public static class EventHelper
    {
        public static Dictionary<int, string> FriendlyNames { get; } = new Dictionary<int, string>()
        {
            { EventGroupId.egGoal, "goal" },
            { EventGroupId.egYellow, "yellow" },
            { EventGroupId.egRed, "red" },
            { EventGroupId.egIn, "in" },
            { EventGroupId.egOut, "out" },
            { EventGroupId.egMiss, "miss" },
            { EventGroupId.egAfterGamePenalty, "aftergamepenalty" }
        };

        public static int GetIdByFiendlyName(string name)
        {
            if(FriendlyNames.ContainsValue(name.ToLower()))
            {
                return FriendlyNames.First(n => n.Value.Equals(name, StringComparison.OrdinalIgnoreCase)).Key;
            }

            return -1;
        }
    }
}

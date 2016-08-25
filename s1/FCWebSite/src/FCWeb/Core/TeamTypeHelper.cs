namespace FCWeb.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common.Constants;

    public static class TeamTypeHelper
    {
        public static Dictionary<int, string> FriendlyNames { get; } = new Dictionary<int, string>()
        {
            { TeamTypeId.ttMain, "main" },
            { TeamTypeId.ttReserve, "reserve" },
            { TeamTypeId.ttYoth, "youth" }
        };

        public static int GetIdByFiendlyName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return -1;
            }

            if(FriendlyNames.ContainsValue(name.ToLower()))
            {
                return FriendlyNames.First(n => n.Value.Equals(name, StringComparison.OrdinalIgnoreCase)).Key;
            }

            return -1;
        }
    }
}

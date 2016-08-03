namespace FCWeb.ViewModels.Configuration
{
    using System.Collections.Generic;
    using Core;
    public class OfficeConfiguration : WebConfiguration
    {
        public Dictionary<int, string> EventGroupFriendlyNames
        {
            get
            {
                return EventHelper.FriendlyNames;
            }
        }
    }
}

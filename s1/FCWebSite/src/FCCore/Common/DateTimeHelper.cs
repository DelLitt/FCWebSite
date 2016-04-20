namespace FCCore.Common
{
    using FCCore.Configuration;
    using System;

    public static class DateTimeHelper
    {
        public static DateTime CurrentCountryTime
        {
            get
            {
                return DateTime.UtcNow.AddHours(MainCfg.TimeShift);
            }
        }
    }
}

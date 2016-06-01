namespace FCCore.Common
{
    using FCCore.Configuration;
    using System;

    public static class DateTimeHelper
    {
        public static DateTime CurrentCfgTime
        {
            get
            {
                return DateTime.UtcNow.AddHours(MainCfg.TimeShift);
            }
        }

        public static int GetAge(DateTime birthDate)
        {
            DateTime now = CurrentCfgTime.Date;
            DateTime birth = birthDate.Date;

            int age = now.Year - birth.Year;

            if (birth > now.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}

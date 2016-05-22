namespace FCCore.Common.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventId
    {
        public const int eGoalNormal = 1;
        public const int eGoalPenalty = 2;
        public const int eGoalAuto = 3;
        public const int eGoalTechnical = 4;

        public const int eInStartGame = 31;
        public const int eInSubstitution = 32;
    }

    public class EventGroupId
    {
        public const int egGoal = 1;
        public const int egYellow = 2;
        public const int egRed = 3;
        public const int egIn = 4;
        public const int egOut = 5;
        public const int egMiss = 6;
        public const int egAfterGamePenalty = 7;

        public static IEnumerable<int> egGoals = new int[]
        {
            EventId.eGoalNormal, EventId.eGoalPenalty, EventId.eGoalAuto, EventId.eGoalTechnical
        };
    }
}

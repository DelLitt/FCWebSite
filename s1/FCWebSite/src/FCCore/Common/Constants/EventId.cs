namespace FCCore.Common.Constants
{
    using System.Collections.Generic;

    public class EventId
    {
        public const int eGoalNormal = 1;
        public const int eGoalPenalty = 2;
        public const int eGoalAuto = 3;
        public const int eGoalTechnical = 4;

        public const int eYellowUnknown = 11;
        public const int eYellowRoughing = 12;
        public const int eYellowDangerous = 13;
        public const int eYellowHanding = 14;
        public const int eYellowUnsport = 15;

        public const int eRedUnknown = 21;
        public const int eRedDoubleYellow = 22;
        public const int eRedRoughing = 23;
        public const int eRedLastResort = 24;
        public const int eRedUnsport = 25;
        public const int eRedKeeperHandOfSquad = 26;

        public const int eInStartGame = 31;
        public const int eInSubstitution = 32;

        public const int eOutEndGame = 41;
        public const int eOutSubstitution = 42;
        public const int eOutInjury = 43;

        public const int eMissPenaltyUnknown = 51;
        public const int eMissPenaltyKeeper = 52;
        public const int eMissPenaltyOff = 53;
        public const int eMissPenaltyCarcass = 54;

        public const int eAGPGoal = 61;
        public const int eAGPMissUnknown = 62;
        public const int eAGPKeeper = 63;
        public const int eAGPOff = 64;
        public const int eAGPCarcass = 65;
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

        public static IEnumerable<int> egYellows = new int[]
        {
            EventId.eYellowUnknown, EventId.eRedDoubleYellow, EventId.eYellowDangerous, EventId.eYellowHanding, EventId.eYellowUnsport
        };

        public static IEnumerable<int> egReds = new int[]
        {
            EventId.eRedUnknown, EventId.eYellowRoughing, EventId.eRedRoughing, EventId.eRedLastResort, EventId.eRedUnsport, EventId.eRedKeeperHandOfSquad
        };

        public static IEnumerable<int> egIns = new int[]
        {
            EventId.eInStartGame, EventId.eInSubstitution
        };

        public static IEnumerable<int> egOuts = new int[]
        {
            EventId.eOutEndGame, EventId.eOutSubstitution, EventId.eOutInjury
        };

        public static IEnumerable<int> egMisses = new int[]
        {
            EventId.eMissPenaltyUnknown, EventId.eMissPenaltyKeeper, EventId.eMissPenaltyOff, EventId.eMissPenaltyCarcass
        };

        public static IEnumerable<int> egAfterGamePenalties = new int[]
        {
            EventId.eAGPGoal, EventId.eAGPMissUnknown, EventId.eAGPKeeper, EventId.eAGPOff, EventId.eAGPCarcass
        };
    }
}

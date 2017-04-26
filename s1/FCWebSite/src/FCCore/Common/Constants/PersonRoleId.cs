namespace FCCore.Common.Constants
{
    using System.Collections.Generic;

    public class PersonRoleId
    {
        public const int rrUnknown = 1;

        public const int rrGoalkeeper = 2;
        public const int rrDefender = 3;
        public const int rrMidfielder = 4;
        public const int rrForward = 5;
        public const int rrPositionUnknown = 6;

        public const int rrCoachHead = 11;
        public const int rrCoachAssistant = 12;
        public const int rrCoach = 13;
        public const int rrCoachPhysical = 14;
        public const int rrCoachGoalkeeper = 15;
        public const int rrTeamManager = 31;
        public const int rrCoachVideographer = 55;
        public const int rrCoachYouth = 78;
        public const int rrCoachSenior = 80;
        public const int rrCoachScientificAndMethodical = 82;
        public const int rrCoachAdministrator = 35;

        public const int rrMedicalWorker = 21;
        public const int rrDoctor = 22;
        public const int rrMassagist = 23;
        
        public const int rrClubPresident = 32;
        public const int rrClubDirector = 33;
        public const int rrViceDirector = 83;
        public const int rrClubChairman = 34;
        public const int rrViceChairman = 86;

        public const int rrClubSpecialist = 51;
        public const int rrAccountantChief = 52;
        public const int rrSpecialistSafety = 54;

        public const int rrAccountant = 53;
        public const int rrSpecialistTechnical = 56;
        public const int rrPressOfficer = 57;
        public const int rrStaffSpecialist = 58;
        public const int rrEconomist = 77;

        public const int rrReferee = 71;
        public const int rrRefereeChief = 72;
        public const int rrRefereeAssistant = 73;
        public const int rrInspector = 74;
        public const int rrRefereeReserve = 75;
        public const int rrRefereeBehindGoal = 76;


        public int Unknown { get { return rrUnknown; } }

        public int PlayerGoalkeeper { get { return rrGoalkeeper; } }
        public int PlayerDefender { get { return rrDefender; } }
        public int PlayerMidfielder { get { return rrMidfielder; } }
        public int PlayerForward { get { return rrForward; } }
        public int PlayerPositionUnknown { get { return rrPositionUnknown; } }

        public int CoachHead { get { return rrCoachHead; } }
        public int CoachAssistant { get { return rrCoachAssistant; } }
        public int Coach { get { return rrCoach; } }
        public int CoachPhysical { get { return rrCoachPhysical; } }
        public int CoachGoalkeeper { get { return rrCoachGoalkeeper; } }
        public int CoachYouth { get { return rrCoachYouth; } }
        public int CoachSenior { get { return rrCoachSenior; } }
        public int CoachScientificAndMethodical { get { return rrCoachScientificAndMethodical; } }
    }

    public class PersonRoleGroupId
    {
        public const int rUnknown = 1;
        public const int rTeamPlayer = 2;
        public const int rCoachingStaff = 3;
        public const int rMedicalStaff = 4;
        public const int rSeniorStaff = 5;
        public const int rSspecialistsStuff = 6;
        public const int rReferee = 7;

        public static IEnumerable<int> rgUnknown = new int[]
        {
            PersonRoleId.rrUnknown
        };

        public static IEnumerable<int> rgTeamPlayer = new int[]
        {
            PersonRoleId.rrGoalkeeper, PersonRoleId.rrDefender, PersonRoleId.rrMidfielder, PersonRoleId.rrForward, PersonRoleId.rrPositionUnknown
        };

        public static IEnumerable<int> rgTeamPitchPlayer = new int[]
        {
            PersonRoleId.rrDefender, PersonRoleId.rrMidfielder, PersonRoleId.rrForward, PersonRoleId.rrPositionUnknown
        };

        public static IEnumerable<int> rgCoachingStaff = new int[]
        {
            PersonRoleId.rrCoachHead, PersonRoleId.rrCoachAssistant, PersonRoleId.rrCoach, PersonRoleId.rrCoachPhysical, PersonRoleId.rrCoachGoalkeeper,
            PersonRoleId.rrCoachYouth, PersonRoleId.rrCoachSenior, PersonRoleId.rrCoachScientificAndMethodical, PersonRoleId.rrTeamManager,
            PersonRoleId.rrCoachAdministrator, PersonRoleId.rrCoachVideographer
        };

        public static IEnumerable<int> rgMedicalStaff = new int[]
        {
            PersonRoleId.rrMedicalWorker, PersonRoleId.rrDoctor, PersonRoleId.rrMassagist
        };

        public static IEnumerable<int> rgSeniorStaff = new int[]
        {
            PersonRoleId.rrClubPresident, PersonRoleId.rrClubDirector, PersonRoleId.rrClubChairman, PersonRoleId.rrViceDirector, PersonRoleId.rrViceChairman,
            PersonRoleId.rrAccountantChief
        };

        public static IEnumerable<int> rgSpecialistsStuff = new int[]
        {
            PersonRoleId.rrClubSpecialist, PersonRoleId.rrAccountant, PersonRoleId.rrSpecialistSafety, PersonRoleId.rrSpecialistTechnical, PersonRoleId.rrPressOfficer,
            PersonRoleId.rrStaffSpecialist, PersonRoleId.rrEconomist
        };

        public static IEnumerable<int> rgReferee = new int[]
        {
            PersonRoleId.rrReferee, PersonRoleId.rrRefereeChief, PersonRoleId.rrRefereeAssistant, PersonRoleId.rrInspector, PersonRoleId.rrRefereeReserve,
            PersonRoleId.rrRefereeBehindGoal
        };
    }
}

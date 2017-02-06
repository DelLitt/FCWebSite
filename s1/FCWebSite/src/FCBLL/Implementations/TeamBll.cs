namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;
    using Newtonsoft.Json;

    public class TeamBll : FCBllBase, ITeamBll
    {
        public bool? Active
        {
            get
            {
                return DalTeam.Active;
            }

            set
            {
                DalTeam.Active = value;
            }
        }

        public bool FillCities { get { return DalTeam.FillCities; } set { DalTeam.FillCities = value; } }
        public bool FillMainTourney { get { return DalTeam.FillMainTourney; } set { DalTeam.FillMainTourney = value; } }
        public bool FillStadium { get { return DalTeam.FillStadium; } set { DalTeam.FillStadium = value; } }
        public bool FillTeamType { get { return DalTeam.FillTeamType; } set { DalTeam.FillTeamType = value; } }

        private ITeamDal dalTeam;
        private ITeamDal DalTeam
        {
            get
            {
                if (dalTeam == null)
                {
                    dalTeam = DALFactory.Create<ITeamDal>();
                }

                return dalTeam;
            }
        }

        private IRoundDal dalRound;
        private IRoundDal DalRound
        {
            get
            {
                if (dalRound == null)
                {
                    dalRound = DALFactory.Create<IRoundDal>(DalTeam);
                }

                return dalRound;
            }
        }

        public Team GetTeam(int id)
        {
            return DalTeam.GetTeam(id);
        }

        public IEnumerable<Team> GetTeamsByRound(int roundId)
        {
            string cacheKey = GetStringMethodKey(nameof(GetTeamsByRound), roundId);

            IEnumerable<Team> result = Cache.GetOrCreate(cacheKey, () => { return GetTeamsByRoundPrivate(roundId); });

            return result;
        }

        private IEnumerable<Team> GetTeamsByRoundPrivate(int roundId)
        {
            Round round = DalRound.GetRound(roundId);

            if (round == null)
            {
                return new Team[0];
            }

            if (string.IsNullOrWhiteSpace(round.TeamList))
            {
                return DalTeam.GetTeams();
            }

            var teamIds = JsonConvert.DeserializeObject<int[]>(round.TeamList);

            if (teamIds == null)
            {
                return new Team[0];
            }

            if (teamIds.Length == 0)
            {
                return DalTeam.GetTeams();
            }

            return DalTeam.GetTeams(teamIds);
        }

        public IEnumerable<Team> GetTeamsByType(int teamTypeId)
        {
            return DalTeam.GetTeamsByType(teamTypeId);
        }

        public IEnumerable<Team> GetTeamsByTypeAndParent(int teamTypeId, int parentTeamId)
        {
            return DalTeam.GetTeamsByTypeAndParent(teamTypeId, parentTeamId);
        }

        public IEnumerable<Team> GetTeams(IEnumerable<int> ids)
        {
            string cacheKey = GetStringKey("GetTeams", ids);

            IEnumerable<Team> result = Cache.GetOrCreate(cacheKey, () => { return DalTeam.GetTeams(ids); });

            return result;
        }

        public IEnumerable<Team> GetTeams()
        {
            return DalTeam.GetTeams();
        }

        public IEnumerable<Team> SearchByDefault(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Team[0]; }

            return DalTeam.SearchByDefault(text);
        }

        public IEnumerable<Team> SearchByDefault(string text, int roundId)
        {
            Round round = DalRound.GetRound(roundId);

            if(round == null)
            {
                return new Team[0];
            }

            if(string.IsNullOrWhiteSpace(round.TeamList))
            {
                return SearchByDefault(text);
            }

            var teamIds = JsonConvert.DeserializeObject<int[]>(round.TeamList);

            if(teamIds == null)
            {
                return new Team[0];
            }

            if(teamIds.Length == 0)
            {
                return SearchByDefault(text);
            }

            return DalTeam.SearchByDefault(text, teamIds);
        }

        public int SaveTeam(Team entity)
        {
            return DalTeam.SaveTeam(entity);
        }
    }
}

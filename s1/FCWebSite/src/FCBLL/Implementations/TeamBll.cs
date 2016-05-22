namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;
    using Newtonsoft.Json;

    public class TeamBll : ITeamBll
    {
        public bool FillCities
        {
            get
            {
                return DalTeam.FillCities;
            }

            set
            {
                DalTeam.FillCities = value;
            }
        }

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
            Round round = DalRound.GetRound(roundId);

            if (round == null)
            {
                return new Team[0];
            }

            if (string.IsNullOrWhiteSpace(round.TeamList))
            {
                return DalTeam.GetAll();
            }

            var teamIds = JsonConvert.DeserializeObject<int[]>(round.TeamList);

            if (teamIds == null)
            {
                return new Team[0];
            }

            if (teamIds.Length == 0)
            {
                return DalTeam.GetAll();
            }

            return DalTeam.GetTeams(teamIds);
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

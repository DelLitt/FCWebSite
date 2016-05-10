namespace FCBLL.Implemetations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class TeamBll : ITeamBll
    {
        private ITeamDal dalTeam;
        private ITeamDal DALTeam
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

        public Team GetTeam(int id)
        {
            return DALTeam.GetTeam(id);
        }

        public IEnumerable<Team> SearchByName(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Team[0]; }

            return DALTeam.SearchByName(text);
        }

        public int SaveTeam(Team entity)
        {
            return DALTeam.SaveTeam(entity);
        }
    }
}

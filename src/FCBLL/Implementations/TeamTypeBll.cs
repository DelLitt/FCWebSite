namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class TeamTypeBll : ITeamTypeBll
    {
        private ITeamTypeDal dalTeamType;
        private ITeamTypeDal DalTeamType
        {
            get
            {
                if (dalTeamType == null)
                {
                    dalTeamType = DALFactory.Create<ITeamTypeDal>();
                }

                return dalTeamType;
            }
        }

        public TeamType GetTeamType(int id)
        {
            return DalTeamType.GetTeamType(id);
        }

        public IEnumerable<TeamType> GetTeamTypes()
        {
            return DalTeamType.GetTeamTypes();
        }
    }
}

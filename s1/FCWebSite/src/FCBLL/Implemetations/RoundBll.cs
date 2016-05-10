namespace FCBLL.Implemetations
{
    using FCCore.Abstractions.Bll;
    using System.Collections.Generic;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public class RoundBll : IRoundBll
    {
        public bool FillTourneys
        {
            get
            {
                return DalRound.FillTourneys;
            }

            set
            {
                DalRound.FillTourneys = value;
            }
        }

        private IRoundDal dalRound;
        private IRoundDal DalRound
        {
            get
            {
                if (dalRound == null)
                {
                    dalRound = DALFactory.Create<IRoundDal>();
                }

                return dalRound;
            }
        }

        public IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = default(int?))
        {
            return DalRound.GetRoundIdsOfTourneys(tourneyIds, sortByTeamId);
        }

        public IEnumerable<Round> GetRounds(IEnumerable<int> ids)
        {
            return DalRound.GetRounds(ids);
        }
    }
}

namespace FCBLL.Implementations
{
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class TotalizatorBll: ITotalizatorBll
    {
        private ITotalizatorDal dalTotalizator;
        private ITotalizatorDal DalTotalizator
        {
            get
            {
                if (dalTotalizator == null)
                {
                    dalTotalizator = DALFactory.Create<ITotalizatorDal>();
                }

                return dalTotalizator;
            }
        }

        public ToteResult AddVote(int gameId, short voteType, string userIP)
        {
            return DalTotalizator.AddVote(gameId, voteType, userIP);
        }

        public ToteResult GetResult(int gameId)
        {
            return DalTotalizator.GetResult(gameId);
        }

        public bool IsUserVoted(int gameId, string userIP)
        {
            return DalTotalizator.IsUserVoted(gameId, userIP);
        }
    }
}

namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;
    using Implementations;

    public class TotalizatorDal : DalBase, ITotalizatorDal
    {
        public ToteResult AddVote(int gameId, short voteType, string userIP)
        {
            if(Context.Totalizator.Any(t => t.gameId == gameId  && t.UserIP.Equals(userIP)))
            {
                return null;
            }

            Context.Totalizator.Add(new Totalizator
            {
                gameId = gameId,
                voteType = voteType,
                UserIP = userIP
            });

            Context.SaveChanges();

            return GetResult(gameId);
        }

        public ToteResult GetResult(int gameId)
        {
            IEnumerable<Totalizator> allVotes = Context.Totalizator.Where(t => t.gameId == gameId);

            var result = new ToteResult();

            if(allVotes.Any())
            {
                result.WinsCount = allVotes.Where(v => v.voteType == 0).Count();
                result.DrawsCount = allVotes.Where(v => v.voteType == 1).Count();
                result.LosesCount = allVotes.Where(v => v.voteType == 2).Count();
            }

            return result;
        }

        public bool IsUserVoted(int gameId, string userIP)
        {
            return Context.Totalizator.Any(t => t.gameId == gameId && t.UserIP.Equals(userIP));
        }
    }
}

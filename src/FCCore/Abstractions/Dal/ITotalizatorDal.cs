namespace FCCore.Abstractions.Dal
{
    using Model;

    public interface ITotalizatorDal : IDalBase
    {
        ToteResult AddVote(int gameId, short voteType, string userIP);
        ToteResult GetResult(int gameId);
        bool IsUserVoted(int gameId, string userIP);
    }
}

namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class RoundBll : FCBllBase, IRoundBll
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

        public bool FillGames
        {
            get
            {
                return DalRound.FillGames;
            }

            set
            {
                DalRound.FillGames = value;
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

        public Round GetRound(int id)
        {
            return DalRound.GetRound(id);
        }

        public IEnumerable<Round> GetRounds(IEnumerable<int> ids)
        {
            return DalRound.GetRounds(ids);
        }

        public IEnumerable<Round> GetRoundsByTourney(int tourneyId)
        {
            return DalRound.GetRoundsByTourney(tourneyId);
        }

        public IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = default(int?))
        {
            return DalRound.GetRoundIdsOfTourneys(tourneyIds, sortByTeamId);
        }

        public IEnumerable<Round> GetRoundsOfTourneys(IEnumerable<int> tourneyIds)
        {
            string cacheKey = GetStringMethodKey(nameof(GetRoundsOfTourneys), tourneyIds);

            IEnumerable<Round> result = Cache.GetOrCreate(cacheKey, () => { return DalRound.GetRoundsOfTourneys(tourneyIds); });

            return result;
        }

        public IEnumerable<Round> SearchByNameFull(int tourneyId, string text)
        {
            return DalRound.SearchByNameFull(tourneyId, text);
        }

        public Round SaveRound(Round entity)
        {
            return DalRound.SaveRound(entity);
        }

        public int RemoveRound(int roundId, bool removeGames = true)
        {
            return DalRound.RemoveRound(roundId, removeGames);
        }
    }
}

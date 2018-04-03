namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class TourneyTypeBll : FCBllBase, ITourneyTypeBll
    {
        private ITourneyTypeDal dalTourneyType;
        private ITourneyTypeDal DalTourneyType
        {
            get
            {
                if (dalTourneyType == null)
                {
                    dalTourneyType = DALFactory.Create<ITourneyTypeDal>();
                }

                return dalTourneyType;
            }
        }

        public TourneyType GetTourneyType(int tourneyTypeId)
        {
            return DalTourneyType.GetTourneyType(tourneyTypeId);
        }

        public IEnumerable<TourneyType> GetAll()
        {
            return DalTourneyType.GetAll();
        }

        public IEnumerable<TourneyType> SearchByDefault(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new TourneyType[0]; }

            return DalTourneyType.SearchByDefault(text);
        }
    }
}

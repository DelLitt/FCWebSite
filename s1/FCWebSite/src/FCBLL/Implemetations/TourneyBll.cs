namespace FCBLL.Implemetations
{
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public sealed class TourneyBll : ITourneyBll
    {
        private ITourneyDal dalTourney;
        private ITourneyDal DalTourney
        {
            get
            {
                if (dalTourney == null)
                {
                    dalTourney = DALFactory.Create<ITourneyDal>();
                }

                return dalTourney;
            }
        }

        public Tourney GetTourney(int tourneyId)
        {
            return DalTourney.GetTourney(tourneyId);
        }
    }
}

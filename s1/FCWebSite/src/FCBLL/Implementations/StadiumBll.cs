namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class StadiumBll : IStadiumBll
    {
        public bool FillCities
        {
            get
            {
                return DalStadium.FillCities;
            }

            set
            {
                DalStadium.FillCities = value;
            }
        }

        private IStadiumDal dalStadium;
        private IStadiumDal DalStadium
        {
            get
            {
                if (dalStadium == null)
                {
                    dalStadium = DALFactory.Create<IStadiumDal>();
                }

                return dalStadium;
            }
        }

        public Stadium GetStadium(int id)
        {
            return DalStadium.GetStadium(id);
        }

        public IEnumerable<Stadium> GetAll()
        {
            return DalStadium.GetAll();
        }

        public IEnumerable<Stadium> SearchByNameFull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Stadium[0]; }

            return DalStadium.SearchByNameFull(text);
        }

        public int SaveStadium(Stadium entity)
        {
            return DalStadium.SaveStadium(entity);
        }
    }
}

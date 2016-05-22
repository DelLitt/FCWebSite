namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class CityBll : ICityBll
    {
        private ICityDal dalCity;
        private ICityDal DALCity
        {
            get
            {
                if (dalCity == null)
                {
                    dalCity = DALFactory.Create<ICityDal>();
                }

                return dalCity;
            }
        }

        public City GetCity(int id)
        {
            return DALCity.GetCity(id);
        }

        public IEnumerable<City> GetCities(IEnumerable<int> ids)
        {
            return DALCity.GetCities(ids);
        }

        public IEnumerable<City> GetAll()
        {
            return DALCity.GetAll();
        }

        public IEnumerable<City> SearchByNameFull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new City[0]; }

            return DALCity.SearchByNameFull(text);
        }
    }
}

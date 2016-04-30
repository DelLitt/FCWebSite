namespace FCDAL.Implemetations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;

    public class CityDal : DalBase, ICityDal
    {
        public City GetCity(int id)
        {
            return Context.City.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<City> GetAll()
        {
            return Context.City;
        }

        public IEnumerable<City> SearchByNameFull(string text)
        {
            return Context.City.Where(v => v.NameFull.Contains(text));
        }
    }
}

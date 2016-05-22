namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using FCCore.Model;

    public class StadiumDal : DalBase, IStadiumDal
    {
        public bool FillCities { get; set; } = false;

        public Stadium GetStadium(int id)
        {
            Stadium stadium = Context.Stadium.FirstOrDefault(p => p.Id == id);

            if(stadium != null)
            {
                FillRelations(new Stadium[] { stadium });
            }

            return stadium;
        }

        public IEnumerable<Stadium> GetAll()
        {
            IEnumerable<Stadium> stadiums = Context.Stadium.ToList();

            FillRelations(stadiums);

            return stadiums;
        }

        public IEnumerable<Stadium> SearchByNameFull(string text)
        {
            IEnumerable<Stadium> stadiums = Context.Stadium.Where(v => v.NameFull.Contains(text) || v.city.NameFull.Contains(text));

            FillRelations(stadiums);

            return stadiums;
        }

        public int SaveStadium(Stadium entity)
        {
            if (entity.Id > 0)
            {
                Context.Stadium.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Stadium.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }

        private void FillRelations(IEnumerable<Stadium> stadiums)
        {
            if (Guard.IsEmptyIEnumerable(stadiums)) { return; }

            IEnumerable<City> cities = new City[0];

            if (FillCities)
            {
                var citiesDal = new CityDal();
                citiesDal.SetContext(Context);

                var citiesIds = new List<int>();
                citiesIds.AddRange(stadiums.Select(r => (int)r.cityId));

                cities = citiesDal.GetCities(citiesIds.Distinct()).ToList();

                if (!cities.Any())
                {
                    throw new DalMappingException(nameof(cities), typeof(Round));
                }
            }

            if (cities.Any())
            {
                foreach (Stadium stadium in stadiums)
                {
                    if (FillCities)
                    {
                        stadium.city = cities.FirstOrDefault(t => t.Id == stadium.cityId);

                        if (stadium.city == null)
                        {
                            throw new DalMappingException(nameof(stadium.city), typeof(Stadium));
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class PersonCareer
    {
        public PersonCareer()
        {
            PersonCareerTourney = new HashSet<PersonCareerTourney>();
        }

        public int Id { get; set; }
        public DateTime? DateFinish { get; set; }
        public DateTime DateStart { get; set; }
        public int personId { get; set; }
        public int teamId { get; set; }

        public virtual ICollection<PersonCareerTourney> PersonCareerTourney { get; set; }
        public virtual Person person { get; set; }
        public virtual Team team { get; set; }
    }
}

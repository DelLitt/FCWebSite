using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class PersonCareerTourney
    {
        public int Id { get; set; }
        public int personCareerId { get; set; }
        public short tourneyId { get; set; }

        public virtual PersonCareer personCareer { get; set; }
        public virtual Tourney tourney { get; set; }
    }
}

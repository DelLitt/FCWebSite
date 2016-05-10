using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Person
    {
        public Person()
        {
            PersonCareer = new HashSet<PersonCareer>();
            PersonStatistics = new HashSet<PersonStatistics>();
            ProtocolRecord = new HashSet<ProtocolRecord>();
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? cityId { get; set; }
        public int? CustomIntValue { get; set; }
        public byte? Height { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string NameMiddle { get; set; }
        public string NameNick { get; set; }
        public byte? Number { get; set; }
        public short? personStatusId { get; set; }
        public short? roleId { get; set; }
        public int? teamId { get; set; }
        public byte? Weight { get; set; }

        public virtual ICollection<PersonCareer> PersonCareer { get; set; }
        public virtual ICollection<PersonStatistics> PersonStatistics { get; set; }
        public virtual ICollection<ProtocolRecord> ProtocolRecord { get; set; }
        public virtual Person PersonNavigation { get; set; }
        public virtual Person InversePersonNavigation { get; set; }
        public virtual City city { get; set; }
        public virtual PersonStatus personStatus { get; set; }
        public virtual PersonRole role { get; set; }
        public virtual Team team { get; set; }
    }
}

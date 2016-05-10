namespace FCWeb.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class PersonViewModel
    {
        public int id { get; set; }
        public bool active { get; set; }
        public DateTime? birthDate { get; set; }
        public int? cityId { get; set; }
        public int? customIntValue { get; set; }
        public byte? height { get; set; }
        public string image { get; set; }
        public string nameFirst { get; set; }
        public string nameLast { get; set; }
        public string nameMiddle { get; set; }
        public string nameNick { get; set; }
        public byte? number { get; set; }
        public short? personStatusId { get; set; }
        public short? roleId { get; set; }
        public int? teamId { get; set; }
        public byte? weight { get; set; }
        public Guid? tempGuid { get; set; }
        public PersonInfoView info { get; set; }
        public IEnumerable<PersonCareerViewModel> career { get; set; } = new PersonCareerViewModel[0];

        //public virtual ICollection<PersonCareer> PersonCareer { get; set; }
        //public virtual ICollection<PersonStatistics> PersonStatistics { get; set; }
        //public virtual Person PersonNavigation { get; set; }
        //public virtual Person InversePersonNavigation { get; set; }
        //public virtual PersonStatus personStatus { get; set; }
        //public virtual PersonRole role { get; set; }
        //public virtual Team team { get; set; }
    }

    public class PersonInfoView
    {
        public string description { get; set; } = string.Empty;
        public PersonCareerView[] career { get; set; } = new PersonCareerView[0];
        public PersonAchievementsView[] achievements { get; set; } = new PersonAchievementsView[0];
    }

    public class PersonCareerView
    {
        public int yearStart { get; set; }
        public int yearEnd { get; set; }
        public string team { get; set; } = string.Empty;

        public PersonCareerView()
        {
            int curYear = DateTime.UtcNow.Year;
            yearStart = curYear;
            yearEnd = curYear;
        }
    }

    public class PersonAchievementsView
    {
        public string season { get; set; } = string.Empty;
        public string team { get; set; } = string.Empty;
        public string achievement { get; set; } = string.Empty;
    }
}

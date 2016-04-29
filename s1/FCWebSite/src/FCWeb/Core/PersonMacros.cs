namespace FCWeb.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels;

    public class PersonMacros
    {
        private const string InfoMacros = "[{~Описание~}]";
        private const string CareerMacros = "[{~Карьера~}]";
        private const string AchievementsMacros = "[{~Достижения~}]";

        //private PersonViewModel personView;

        //public PersonMacros(PersonViewModel personView)
        //{
        //    this.personView = personView;
        //}

        //public string Info
        //{
        //    get
        //    {
        //        return TextBetween(InfoMacros);
        //    }
        //}

        //public string Career
        //{
        //    get
        //    {
        //        return TextBetween(CareerMacros);
        //    }
        //}

        //public string Achievements
        //{
        //    get
        //    {
        //        return TextBetween(AchievementsMacros);
        //    }
        //}

        //private string TextBetween(string tagStart, string tagEnd = null)
        //{
        //    if(string.IsNullOrWhiteSpace(personView.info)) { return string.Empty; }

        //    if(string.IsNullOrWhiteSpace(tagEnd)) { tagEnd = tagStart; }

        //    int posFrom = personView.info.IndexOf(tagStart) + tagStart.Length;
        //    int posTo = personView.info.LastIndexOf(tagEnd);

        //    string result = string.Empty;

        //    if(posFrom >= 0 && posTo > 0 && posFrom < posTo)
        //    {
        //        result = personView.info.Substring(posFrom, posTo - posFrom);
        //    }

        //    return result;
        //}
    }
}

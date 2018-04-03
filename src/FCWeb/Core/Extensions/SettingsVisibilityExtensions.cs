namespace FCWeb.Core.Extensions
{
    using FCCore.Model;
    using ViewModels;

    public static class SettingsVisibilityExtensions
    {
        public static VisibilityViewModel ToViewModel(this SettingsVisibility visibility)
        {
            if (visibility == null) { return null; }

            return new VisibilityViewModel()
            {
                authorized = visibility.Authorized,
                main = visibility.Main,
                news = visibility.News,
                reserve = visibility.Reserve,
                youth = visibility.Youth
            };
        }
    }
}

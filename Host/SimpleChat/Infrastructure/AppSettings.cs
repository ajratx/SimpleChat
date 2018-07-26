namespace SimpleChat.Infrastructure
{
    public class AppSettings
    {
        private static AppSettings instance;

        private AppSettings()
        {
        }

        public static AppSettings Instance => instance ?? new AppSettings();
    }
}

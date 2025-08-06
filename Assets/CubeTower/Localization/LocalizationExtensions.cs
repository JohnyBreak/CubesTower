namespace Localization
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string str)
        {
            return TranslationManager.Translate(str);
        }
    }
}


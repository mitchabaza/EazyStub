namespace Latsos.Client
{
    public static class Settings
    {
        public static string Url { get; private set; }
        public static void SetServerUrl(string url)
        {
            Url = url;
        }        
    }
}
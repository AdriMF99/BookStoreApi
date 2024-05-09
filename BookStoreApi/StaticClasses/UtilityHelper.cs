using MongoDB.Driver;

namespace BookStoreApi.StaticClasses
{
    public class UtilityHelper
    {
        // GET APPSETTINGS
        public static string GetAppSetting(string value)
        {
            if (ApplicationValues.secretValues.ContainsKey(value))
            {
                return ApplicationValues.secretValues.GetValueOrDefault(value, "");
            }
            return "";
        }

        // GET MONGO DB CONNECTION
        public static MongoClientSettings GetMongoDbConnection()
        {
            var settings = MongoClientSettings.FromConnectionString(GetAppSetting("DBConnect"));
            settings.SslSettings = new SslSettings() {  EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
            return settings;
        }
    }
}

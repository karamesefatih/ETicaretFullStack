using Microsoft.Extensions.Configuration;

namespace Persistence
{
    static class Configuration
    {
        // bu static class appsetting.json dosyasından connection string değerini alıp çağırılan diğer yerlere string olarak verecek
        // Microsoft.Extensions.Configuration paketi jsondan değer almamızı sağlıyor burada
         static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSql");
            }
        }
    }
}

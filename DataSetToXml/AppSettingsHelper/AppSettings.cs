using System.Configuration;

namespace DataSetToXml.AppSettingsHelper
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString(string name) => GetConnectionString(name);

        string GetConnectionString(string name)
        {
            var value = ConfigurationManager.ConnectionStrings[name].ConnectionString;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ConfigurationErrorsException($"ConnectionString named '{name}' was not configured in the configuration file.");
            }

            return value;
        }

    }
}

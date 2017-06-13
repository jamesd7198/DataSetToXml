using System;
using System.Configuration;

namespace DataSetToXml.AppSettingsHelper
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString(string name) => GetConnectionString(name);

        string GetConnectionString(string name)
        {
            var connStr = string.Empty;

            try
            {
                connStr = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch
            {
                // ignored
            }

            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ConfigurationErrorsException($"ConnectionString named '{name}' was not configured in the configuration file.");
            }

            return connStr;
        }

    }
}

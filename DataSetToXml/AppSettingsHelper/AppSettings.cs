using System.ComponentModel;
using System.Configuration;

namespace DataSetToXml.AppSettingsHelper
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString(string name)
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

        public string AppSetting(string name)
        {
            return AppSetting<string>(name);
        }

        public T AppSetting<T>(string name)
        {
            var value = string.Empty;

            try
            {
                value = ConfigurationManager.AppSettings[name];
            }
            catch
            {
                // ignored
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ConfigurationErrorsException($"AppSetting key '{name}' was not configured in the configuration file.");
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromInvariantString(value);
        }
    }
}

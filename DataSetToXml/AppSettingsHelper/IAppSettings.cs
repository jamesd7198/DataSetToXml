namespace DataSetToXml.AppSettingsHelper
{
    public interface IAppSettings
    {
        string ConnectionString(string name);
        string AppSetting(string name);
        T AppSetting<T>(string name);
    }

}

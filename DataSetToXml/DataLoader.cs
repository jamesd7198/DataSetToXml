using System.Data;
using System.IO;
using DataSetToXml.DataSettingsHelper;

namespace DataSetToXml
{
    public class DataLoader
    {
        public static DataSet LoadTableFromXml(Stream xmlSchemaStream, Stream xmlDataStream)
        {
            var dataSet = new DataSet { EnforceConstraints = false };

            dataSet.ReadXmlSchema(xmlSchemaStream);
            dataSet.ReadXml(xmlDataStream);

            return dataSet;
        }
        
        public static DataSet LoadDataSetFromQuery(IDataSettings dataSettings, string sql)
        {
            var dataSet = new DataSet();

            using (dataSettings)
            {
                dataSettings.Command.Connection = dataSettings.Connection;
                dataSettings.Command.CommandType = CommandType.Text;
                dataSettings.Command.CommandText = sql;

                dataSettings.Connection.Open();

                dataSettings.Adapter.SelectCommand = dataSettings.Command;
                dataSettings.Adapter.Fill(dataSet);
            }

            return dataSet;
        }

        public static DataSet LoadDataSetFromProcedure(IDataSettings dataSettings, string procedure)
        {
            var dataSet = new DataSet();

            using (dataSettings)
            {
                dataSettings.Command.Connection = dataSettings.Connection;
                dataSettings.Command.CommandType = CommandType.StoredProcedure;
                dataSettings.Command.CommandText = procedure;

                dataSettings.Connection.Open();

                dataSettings.Adapter.SelectCommand = dataSettings.Command;
                dataSettings.Adapter.Fill(dataSet);
            }

            return dataSet;
        }
    }
}

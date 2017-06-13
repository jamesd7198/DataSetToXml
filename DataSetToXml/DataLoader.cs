using System.Data;
using System.IO;
using DataSetToXml.DataSettingsHelper;

namespace DataSetToXml
{
    public class DataLoader
    {
        public static DataTable LoadTableFromXml(Stream xmlSchemaStream, Stream xmlDataStream)
        {
            var dataSet = new DataSet { EnforceConstraints = false };

            dataSet.ReadXmlSchema(xmlSchemaStream);
            dataSet.ReadXml(xmlDataStream);

            return dataSet.Tables[0];
        }

        public static DataTableCollection LoadTableFromDb(IDataSettings dataProvider, string tableName)
        {
            return LoadTableFromQuery(dataProvider, $"select * from {tableName}");
        }

        public static DataTableCollection LoadTableFromQuery(IDataSettings dataProvider, string sql)
        {
            var dataSet = new DataSet();

            using (dataProvider)
            {
                dataProvider.Command.Connection = dataProvider.Connection;
                dataProvider.Command.CommandType = CommandType.Text;
                dataProvider.Command.CommandText = sql;   

                dataProvider.Connection.Open();

                dataProvider.Adapter.SelectCommand = dataProvider.Command;
                dataProvider.Adapter.Fill(dataSet);
            }

            return dataSet.Tables;
        }

        public static DataTableCollection LoadTableFromProcedure(IDataSettings dataProvider, string procedure)
        {
            var dataSet = new DataSet();

            using (dataProvider)
            {
                dataProvider.Command.Connection = dataProvider.Connection;
                dataProvider.Command.CommandType = CommandType.StoredProcedure;
                dataProvider.Command.CommandText = procedure;

                dataProvider.Connection.Open();

                dataProvider.Adapter.SelectCommand = dataProvider.Command;
                dataProvider.Adapter.Fill(dataSet);
            }

            return dataSet.Tables;
        }
    }

}

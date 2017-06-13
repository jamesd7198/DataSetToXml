using System.Data;
using System.IO;
using System.Xml;

namespace DataSetToXml
{
    public class DataWriter
    {
        public static void WriteSchemaToXsdFile(DataTable table, string filepath)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                NewLineOnAttributes = true
            };

            using (var stream = new StreamWriter(filepath))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                table.WriteXmlSchema(writer);
            }
        }

        public static void WriteTableToXmlFile(DataTable table, string filepath)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                NewLineOnAttributes = true
            };

            using (var stream = new StreamWriter(filepath))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                table.WriteXml(writer);
            }
        }
    }

}

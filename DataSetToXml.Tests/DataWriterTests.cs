using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using DataSetToXml.AppSettingsHelper;
using DataSetToXml.DataSettingsHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.BDDfy;

namespace DataSetToXml.Tests
{
    [TestClass]
    public class ShouldWriteLoadedDataToXmlFile
    {
        [TestMethod]
        public void VerifyXmlFileIsCreated()
        {
            this.Given(t => GivenSqlDbTableWith3Rows())
                .And(t => GivenDbTableIsLoaded())
                .When(t => WhenWritingTableToXmlFile())
                .Then(t => ThenXmlFileShouldExist())
                .And(t => ThenXmlFileShouldHaveFordChevyAndDodge())
                .BDDfy();
        }

        void GivenSqlDbTableWith3Rows()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new SqlConnection(_appSettings.ConnectionString("SqlDb")),
                new SqlCommand(),
                new SqlDataAdapter());
        }

        void GivenDbTableIsLoaded()
        {
            _dataTable = DataLoader.LoadTableFromDb(_dataSettings, "test_makes")[0];
        }

        void WhenWritingTableToXmlFile()
        {
            DataWriter.WriteTableToXmlFile(_dataTable, @"C:\temp\test_makes.xml");
        }

        void ThenXmlFileShouldExist()
        {
            var fileInfo = new FileInfo(@"C:\temp\test_makes.xml");

            Assert.IsTrue(fileInfo.Exists);
        }

        void ThenXmlFileShouldHaveFordChevyAndDodge()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\temp\test_makes.xml");

            var node = xmlDoc.SelectSingleNode("//Table[name='Ford']");

            Assert.IsNotNull(node);
            Assert.AreEqual("Ford", node.ChildNodes[1].InnerText);
        }

        DataTable _dataTable;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

    [TestClass]
    public class ShouldWriteSchemaToXsdFile
    {
        [TestMethod]
        public void VerifyXsdFileIsCreated()
        {
            this.Given(t => GivenSqlDbTableWith3Rows())
                .And(t => GivenDbTableIsLoaded())
                .When(t => WhenWritingSchemaToXsdFile())
                .Then(t => ThenXsdFileShouldExist())
                .BDDfy();
        }

        void GivenSqlDbTableWith3Rows()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new SqlConnection(_appSettings.ConnectionString("SqlDb")),
                new SqlCommand(),
                new SqlDataAdapter());
        }

        void GivenDbTableIsLoaded()
        {
            _dataTable = DataLoader.LoadTableFromDb(_dataSettings, "test_makes")[0];
        }

        void WhenWritingSchemaToXsdFile()
        {
            DataWriter.WriteSchemaToXsdFile(_dataTable, @"C:\temp\test_makes.xsd");
        }

        void ThenXsdFileShouldExist()
        {
            var fileInfo = new FileInfo(@"C:\temp\test_makes.xsd");

            Assert.IsTrue(fileInfo.Exists);
        }
        
        DataTable _dataTable;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

}

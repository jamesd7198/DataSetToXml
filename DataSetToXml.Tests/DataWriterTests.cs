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
    public class ShouldWriteDataSetToXmlFile
    {
        [TestMethod]
        public void VerifyXmlFileIsCreatedByQuery()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .And(t => GivenDataSetIsLoadedByQuery())
                .When(t => WhenWritingDataSetToXmlFile(@"C:\temp\test_makes.xml"))
                .Then(t => ThenXmlFileShouldExist())
                .And(t => ThenXmlFileShouldHaveFord())
                .BDDfy();
        }

        [TestMethod]
        public void VerifyXmlFileIsCreatedByProcedure()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .And(t => GivenDataSetIsLoadedByProcedure())
                .When(t => WhenWritingDataSetToXmlFile(@"C:\temp\test_makes.xml"))
                .Then(t => ThenXmlFileShouldExist())
                .And(t => ThenXmlFileShouldHaveF150())
                .BDDfy();
        }

        void GivenSqlDbWithMakesAndModels()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new SqlConnection(_appSettings.ConnectionString("SqlDb")),
                new SqlCommand(),
                new SqlDataAdapter());
        }

        void GivenDataSetIsLoadedByQuery()
        {
            _dataSet = DataLoader.LoadDataSetFromQuery(_dataSettings, "select * from test_makes");
        }

        void GivenDataSetIsLoadedByProcedure()
        {
            _dataSettings.AddParameter(new SqlParameter("@make", "Ford"));

            _dataSet = DataLoader.LoadDataSetFromProcedure(_dataSettings, "getModelsByMake");
        }

        void WhenWritingDataSetToXmlFile(string filename)
        {
            DataWriter.WriteDataSetToXmlFile(_dataSet, filename);
        }

        void ThenXmlFileShouldExist()
        {
            Assert.IsTrue(File.Exists(@"C:\temp\test_makes.xml"));
        }

        void ThenXmlFileShouldHaveFord()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\temp\test_makes.xml");

            var node = xmlDoc.SelectSingleNode("//Table[name='Ford']");

            Assert.IsNotNull(node);
            Assert.AreEqual("Ford", node.ChildNodes[1].InnerText);
        }

        void ThenXmlFileShouldHaveF150()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\temp\test_makes.xml");

            var node = xmlDoc.SelectSingleNode("//Table[name='F150']");

            Assert.IsNotNull(node);
            Assert.AreEqual("F150", node.ChildNodes[1].InnerText);
        }

        DataSet _dataSet;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

    [TestClass]
    public class ShouldWriteSchemaToXsdFile
    {
        [TestMethod]
        public void VerifyXsdFileIsCreated()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .And(t => GivenDataSetIsLoadedFromQuery())
                .When(t => WhenWritingSchemaToXsdFile(@"C:\temp\test_makes.xsd"))
                .Then(t => ThenXsdFileShouldExist(@"C:\temp\test_makes.xsd"))
                .BDDfy();
        }

        [TestMethod]
        public void VerifyXsdFileIsCreatedWithMultipleTables()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .And(t => GivenMultipleTablesAreLoadedFromQuery())
                .When(t => WhenWritingSchemaToXsdFile(@"C:\temp\test_makes_and_models.xsd"))
                .Then(t => ThenXsdFileShouldExist(@"C:\temp\test_makes_and_models.xsd"))
                .BDDfy();
        }

        void GivenSqlDbWithMakesAndModels()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new SqlConnection(_appSettings.ConnectionString("SqlDb")),
                new SqlCommand(),
                new SqlDataAdapter());
        }

        void GivenDataSetIsLoadedFromQuery()
        {
            _dataSet = DataLoader.LoadDataSetFromQuery(_dataSettings, "select * from test_makes");
        }

        void GivenMultipleTablesAreLoadedFromQuery()
        {
            _dataSet = DataLoader.LoadDataSetFromQuery(_dataSettings, "select * from test_makes;select * from test_models");
        }

        void WhenWritingSchemaToXsdFile(string filename)
        {
            DataWriter.WriteSchemaToXsdFile(_dataSet, filename);
        }

        void ThenXsdFileShouldExist(string filename)
        {
            Assert.IsTrue(File.Exists(filename));
        }
        
        DataSet _dataSet;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

    [TestClass]
    public class ShouldWriteMultipleTablesToXmlFile
    {
        [TestMethod]
        public void VerifyXmlWith2TablesIsCreatedByQuery()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .And(t => GivenDataSetIsLoadedByQuery())
                .When(t => WhenWritingDataSetToXmlFile(@"C:\temp\two_table_test_query_results.xml"))
                .Then(t => ThenXmlFileShouldExist())
                .BDDfy();
        }

        [TestMethod]
        public void VerifyXmlWith2TablesIsCreatedByProcedure()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .And(t => GivenDataSetIsLoadedByProcedure())
                .When(t => WhenWritingDataSetToXmlFile(@"C:\temp\two_table_test_sproc_results.xml"))
                .Then(t => ThenXmlFileShouldExist())
                .BDDfy();
        }

        void GivenSqlDbWithMakesAndModels()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new SqlConnection(_appSettings.ConnectionString("SqlDb")),
                new SqlCommand(),
                new SqlDataAdapter());
        }

        void GivenDataSetIsLoadedByQuery()
        {
            _dataSet = DataLoader.LoadDataSetFromQuery(_dataSettings, "select * from test_makes; select * from test_models");
        }

        void GivenDataSetIsLoadedByProcedure()
        {
            _dataSet = DataLoader.LoadDataSetFromProcedure(_dataSettings, "getFordAndDodgeInTwoTables");
        }
        
        void WhenWritingDataSetToXmlFile(string filename)
        {
            DataWriter.WriteDataSetToXmlFile(_dataSet, filename);
        }

        void ThenXmlFileShouldExist()
        {
            var fileInfo = new FileInfo(@"C:\temp\makes_and_models.xml");

            Assert.IsTrue(fileInfo.Exists);
        }

        DataSet _dataSet;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

}

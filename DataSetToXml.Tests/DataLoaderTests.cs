using System.Data;
using System.Data.SqlClient;
using DataSetToXml.AppSettingsHelper;
using DataSetToXml.DataSettingsHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.BDDfy;
using Npgsql;

namespace DataSetToXml.Tests
{
    [TestClass]
    public class ShouldLoadFromTableInNpgSqlDb
    {
        [TestMethod]
        public void VerifyTestDataFromTableLoaded()
        {
            this.Given(t => GivenNpgSqlDbTableWith3Rows())
                .When(t => WhenDbTableIsLoaded())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFordChevyAndDodge())
                .BDDfy();
        }

        void GivenNpgSqlDbTableWith3Rows()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new NpgsqlConnection(_appSettings.ConnectionString("NpgSqlDb")), 
                new NpgsqlCommand(),
                new NpgsqlDataAdapter());
        }

        void WhenDbTableIsLoaded()
        {
            _dataTable = DataLoader.LoadTableFromDb(_dataSettings, "test_makes")[0];
        }

        void ThenTableShouldHave3Rows()
        {
            Assert.AreEqual(3, _dataTable.Rows.Count);
        }

        void ThenTableShouldHaveFordChevyAndDodge()
        {
            Assert.AreEqual("Ford", _dataTable.Rows[0].ItemArray[1]);
            Assert.AreEqual("Chevy", _dataTable.Rows[1].ItemArray[1]);
            Assert.AreEqual("Dodge", _dataTable.Rows[2].ItemArray[1]);
        }

        DataTable _dataTable;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

    [TestClass]
    public class ShouldLoadFromQueryInNpgSqlDb
    {
        [TestMethod]
        public void VerifyTestDataFromTableLoaded()
        {
            this.Given(t => GivenNpgSqlDbTableWith3Rows())
                .When(t => WhenQueryResultFor1RowIsLoaded())
                .Then(t => ThenTableShouldHave1Row())
                .And(t => ThenTableShouldHaveFord())
                .BDDfy();
        }

        void GivenNpgSqlDbTableWith3Rows()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new NpgsqlConnection(_appSettings.ConnectionString("NpgSqlDb")),
                new NpgsqlCommand(),
                new NpgsqlDataAdapter());
        }

        void WhenQueryResultFor1RowIsLoaded()
        {
            _dataTable = DataLoader.LoadTableFromQuery(_dataSettings, "select * from test_makes limit 1")[0];
        }

        void ThenTableShouldHave1Row()
        {
            Assert.AreEqual(1, _dataTable.Rows.Count);
        }

        void ThenTableShouldHaveFord()
        {
            Assert.AreEqual("Ford", _dataTable.Rows[0].ItemArray[1]);
        }

        DataTable _dataTable;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

    [TestClass]
    public class ShouldLoadFromProcedureInNpgSqlDb
    {
        [TestMethod]
        public void VerifyTestDataFromTableLoaded()
        {
            this.Given(t => GivenNpgSqlDbTableWith3Rows())
                .When(t => WhenQueryResultFor1RowIsLoaded())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFord())
                .BDDfy();
        }

        void GivenNpgSqlDbTableWith3Rows()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new NpgsqlConnection(_appSettings.ConnectionString("NpgSqlDb")),
                new NpgsqlCommand(),
                new NpgsqlDataAdapter());
        }

        void WhenQueryResultFor1RowIsLoaded()
        {
            _dataTable = DataLoader.LoadTableFromProcedure(_dataSettings, "get_all_fords")[0];
        }

        void ThenTableShouldHave3Rows()
        {
            Assert.AreEqual(3, _dataTable.Rows.Count);
        }

        void ThenTableShouldHaveFord()
        {
            Assert.AreEqual("F150", _dataTable.Rows[0].ItemArray[1]);
            Assert.AreEqual("Mustang", _dataTable.Rows[1].ItemArray[1]);
            Assert.AreEqual("Taurus", _dataTable.Rows[2].ItemArray[1]);
        }

        DataTable _dataTable;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

    [TestClass]
    public class ShouldLoadFromTableInSqlDb
    {
        [TestMethod]
        public void VerifyTestDataFromTableLoaded()
        {
            this.Given(t => GivenSqlDbTableWith3Rows())
                .When(t => WhenDbTableIsLoaded())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFordChevyAndDodge())
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

        void WhenDbTableIsLoaded()
        {
            _dataTable = DataLoader.LoadTableFromDb(_dataSettings, "test_makes")[0];
        }

        void ThenTableShouldHave3Rows()
        {
            Assert.AreEqual(3, _dataTable.Rows.Count);
        }

        void ThenTableShouldHaveFordChevyAndDodge()
        {
            Assert.AreEqual("Ford", _dataTable.Rows[0].ItemArray[1]);
            Assert.AreEqual("Chevy", _dataTable.Rows[1].ItemArray[1]);
            Assert.AreEqual("Dodge", _dataTable.Rows[2].ItemArray[1]);
        }

        DataTable _dataTable;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

}

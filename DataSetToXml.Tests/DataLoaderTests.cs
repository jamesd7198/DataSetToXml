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
    public class ShouldLoadDatSetFromNpgSqlDb
    {
        [TestMethod]
        public void VerifyDataSetLoadedByQuery()
        {
            this.Given(t => GivenNpgSqlDbWithMakesAndModels())
                .When(t => WhenDataSetIsLoadedByQuery())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFordChevyAndDodge())
                .BDDfy();
        }

        [TestMethod]
        public void VerifyDataSetLoadedByProcedure()
        {
            this.Given(t => GivenNpgSqlDbWithMakesAndModels())
                .When(t => WhenDataSetIsLoadedByProcedure())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFordModels())
                .BDDfy();
        }

        //TODO: The current test function in PG database returns both selects from function as one table
        //[TestMethod]
        //public void VerifyMultipleTablesAreLoadedByProcedure()
        //{
        //    this.Given(t => GivenNpgSqlDbWithMakesAndModels())
        //        .When(t => WhenMultipleTablesAreLoadedByProcedure())
        //        .Then(t => ThenDataSetShouldHave2Tables())
        //        .And(t => ThenTableShouldHaveFordModels())
        //        .And(t => ThenSecondTableShouldHaveDodgeModels())
        //        .BDDfy();
        //}

        void GivenNpgSqlDbWithMakesAndModels()
        {
            _appSettings = new AppSettings();

            _dataSettings = new DataSettings(
                new NpgsqlConnection(_appSettings.ConnectionString("NpgSqlDb")),
                new NpgsqlCommand(),
                new NpgsqlDataAdapter());
        }

        void WhenDataSetIsLoadedByQuery()
        {
            _dataSet = DataLoader.LoadDataSetFromQuery(_dataSettings, "select * from test_makes");
        }

        void WhenDataSetIsLoadedByProcedure()
        {
            _dataSettings.AddParameter(new NpgsqlParameter("make", "Ford"));

            _dataSet = DataLoader.LoadDataSetFromProcedure(_dataSettings, "getModelsByMake");
        }

        void WhenMultipleTablesAreLoadedByProcedure()
        {
            _dataSet = DataLoader.LoadDataSetFromProcedure(_dataSettings, "getFordAndDodgeInTwoTables");
        }

        void ThenTableShouldHave3Rows()
        {
            var table = _dataSet.Tables[0];

            Assert.AreEqual(3, table.Rows.Count);
        }

        void ThenTableShouldHaveFordChevyAndDodge()
        {
            var table = _dataSet.Tables[0];

            Assert.AreEqual("Ford", table.Rows[0].ItemArray[1]);
            Assert.AreEqual("Chevy", table.Rows[1].ItemArray[1]);
            Assert.AreEqual("Dodge", table.Rows[2].ItemArray[1]);
        }

        void ThenTableShouldHaveFordModels()
        {
            var table = _dataSet.Tables[0];

            Assert.AreEqual("F150", table.Rows[0].ItemArray[1]);
            Assert.AreEqual("Mustang", table.Rows[1].ItemArray[1]);
            Assert.AreEqual("Taurus", table.Rows[2].ItemArray[1]);
        }

        void ThenDataSetShouldHave2Tables()
        {
            Assert.AreEqual(2, _dataSet.Tables.Count);
        }

        void ThenSecondTableShouldHaveDodgeModels()
        {
            var table = _dataSet.Tables[1];

            Assert.AreEqual("Ram", table.Rows[0].ItemArray[1]);
            Assert.AreEqual("Challenger", table.Rows[1].ItemArray[1]);
            Assert.AreEqual("Charger", table.Rows[2].ItemArray[1]);
        }

        DataSet _dataSet;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }
   
    [TestClass]
    public class ShouldLoadDatSetFromSqlDb
    {
        [TestMethod]
        public void VerifyDataSetLoadedByQuery()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .When(t => WhenDataSetIsLoadedByQuery())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFordChevyAndDodge())
                .BDDfy();
        }

        [TestMethod]
        public void VerifyDataSetLoadedByProcedure()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .When(t => WhenDataSetIsLoadedByProcedure())
                .Then(t => ThenTableShouldHave3Rows())
                .And(t => ThenTableShouldHaveFordModels())
                .BDDfy();
        }

        [TestMethod]
        public void VerifyMultipleTablesAreLoadedByProcedure()
        {
            this.Given(t => GivenSqlDbWithMakesAndModels())
                .When(t => WhenMultipleTablesAreLoadedByProcedure())
                .Then(t => ThenDataSetShouldHave2Tables())
                .And(t => ThenTableShouldHaveFordModels())
                .And(t => ThenSecondTableShouldHaveDodgeModels())
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

        void WhenDataSetIsLoadedByQuery()
        {
            _dataSet = DataLoader.LoadDataSetFromQuery(_dataSettings, "select * from test_makes");
        }

        void WhenDataSetIsLoadedByProcedure()
        {
            _dataSettings.AddParameter(new SqlParameter("@make", "Ford"));

            _dataSet = DataLoader.LoadDataSetFromProcedure(_dataSettings, "getModelsByMake");
        }

        void WhenMultipleTablesAreLoadedByProcedure()
        {
            _dataSet = DataLoader.LoadDataSetFromProcedure(_dataSettings, "getFordAndDodgeInTwoTables");
        }

        void ThenTableShouldHave3Rows()
        {
            var table = _dataSet.Tables[0];

            Assert.AreEqual(3, table.Rows.Count);
        }

        void ThenTableShouldHaveFordChevyAndDodge()
        {
            var table = _dataSet.Tables[0];

            Assert.AreEqual("Ford", table.Rows[0].ItemArray[1]);
            Assert.AreEqual("Chevy", table.Rows[1].ItemArray[1]);
            Assert.AreEqual("Dodge", table.Rows[2].ItemArray[1]);
        }

        void ThenTableShouldHaveFordModels()
        {
            var table = _dataSet.Tables[0];

            Assert.AreEqual("F150", table.Rows[0].ItemArray[1]);
            Assert.AreEqual("Mustang", table.Rows[1].ItemArray[1]);
            Assert.AreEqual("Taurus", table.Rows[2].ItemArray[1]);
        }

        void ThenDataSetShouldHave2Tables()
        {
            Assert.AreEqual(2, _dataSet.Tables.Count);
        }

        void ThenSecondTableShouldHaveDodgeModels()
        {
            var table = _dataSet.Tables[1];

            Assert.AreEqual("Ram", table.Rows[0].ItemArray[1]);
            Assert.AreEqual("Challenger", table.Rows[1].ItemArray[1]);
            Assert.AreEqual("Charger", table.Rows[2].ItemArray[1]);
        }

        DataSet _dataSet;
        IAppSettings _appSettings;
        IDataSettings _dataSettings;
    }

}

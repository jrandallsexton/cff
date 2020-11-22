using Cff.Core.Data;
using Cff.Core.Engines;
using Cff.Core.Enumerations;
using Cff.Core.Implementations;
using Cff.Core.Interfaces;
using Cff.Core.Models;
using Cff.Core.Services;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Cff.Core.Tests.Misc
{

    /// <summary>
    /// Note:  The methods in here are for experimentation - not actual unit testing
    /// The NUnit framework is simply here as a way to easily execute the code
    /// </summary>
    [TestFixture]
    public class TestCases
    {

        private IForecastHelper _helper;
        private IForecastEngine _engine;

        private const string DbConnectionStringName = "cffdb";

        [SetUp]
        public void CreateHelper()
        {
            _helper = new ForecastHelper(false, false);
            _engine = new RoughEngine();
            _engine.IsVerbose(true);
        }

        [Test]
        public void GetExistingForecast()
        {
            var cffSvc = new CffService(new CffContext(GetDataContextOptions()));

            var forecast = cffSvc.Get(1);
            Assert.NotNull(forecast);
        }

        private static DbContextOptions<CffContext> GetDataContextOptions()
        {
            // https://stackoverflow.com/questions/52810039/moq-and-setting-up-db-context
            var dbName = Guid.NewGuid().ToString().Substring(0, 5);
            return new DbContextOptionsBuilder<CffContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        //[Test]
        //public void ProcessExistingForecastModel()
        //{
        //    var cffSvc = new CffService(new CffContext(DbConnectionStringName));

        //    var forecast = cffSvc.Get(1);
        //    Assert.NotNull(forecast);

        //    // stick it in the engine and process it
        //    var result = this._engine.CreateForecast(this._helper, forecast) as ForecastResult;
        //    result.ForecastId = forecast.Id;

        //    cffSvc.SaveForecastResult(result);

        //    PrintForecastResult(forecast.Begin.Year, forecast.Begin.Month, forecast.Begin.Day, forecast, result);
        //}

        [Test]
        public void TestCaseOne()
        {
            var cffSvc = new CffService(new CffContext(GetDataContextOptions()));

            // set-up
            var now = DateTime.Now;

            var yrStart = now.Year;
            var moStart = now.Month;
            var dyStart = now.Day;

            const decimal balanceStart = 3200.00m;

            const int duration = 45;
             
            // Create the forecast
            var forecast = new Forecast("Test Case 1", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration)
            {
                AmountBegin = balanceStart
            };

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.BiWeekly, 3400.00m, now, new DateTime(yrStart, moStart, 20)));
            forecast.AddItem(new ForecastItem("Rental - Income", EForecastItemType.Income, EFrequency.Monthly, 1800.00m, 5));

            forecast.AddItem(new ForecastItem("Rent", EForecastItemType.Expense, EFrequency.BiWeekly, 1050.00m, now, new DateTime(yrStart, moStart, 20)));
            forecast.AddItem(new ForecastItem("Mortgage Chatt", EForecastItemType.Expense, EFrequency.Monthly, 1495.00m, now, new DateTime(yrStart, moStart, 5)));

            forecast.AddItem(new ForecastItem("Internet", EForecastItemType.Expense, EFrequency.Monthly, 95.00m, 10));
            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 250.00m, 18));
            forecast.AddItem(new ForecastItem("Auto Insurance", EForecastItemType.Expense, EFrequency.Monthly, 95.00m, 10));
            forecast.AddItem(new ForecastItem("Boat Insurance", EForecastItemType.Expense, EFrequency.Monthly, 56.00m, 14));
            forecast.AddItem(new ForecastItem("Boat Storage", EForecastItemType.Expense, EFrequency.Monthly, 488.00m, 2));
            forecast.AddItem(new ForecastItem("Afterschool", EForecastItemType.Expense, EFrequency.Weekly, 50.00m, now, new DateTime(yrStart, moStart, 16)));

            forecast.AddItem(new ForecastItem("Rental - Lawn", EForecastItemType.Expense, EFrequency.Monthly, 180.00m, now, new DateTime(yrStart, moStart, 20)));

            forecast.AddItem(new ForecastItem("Netflix", EForecastItemType.Expense, EFrequency.Monthly, 17.50m, 16));
            forecast.AddItem(new ForecastItem("Dropbox", EForecastItemType.Expense, EFrequency.Monthly, 11.99m, 5));
            forecast.AddItem(new ForecastItem("Apple Storage", EForecastItemType.Expense, EFrequency.Monthly, 5.00m, 7));
            forecast.AddItem(new ForecastItem("Google Drive", EForecastItemType.Expense, EFrequency.Monthly, 1.99m, 5));

            forecast.AddItem(new ForecastItem("Barbershop", EForecastItemType.Expense, EFrequency.BiWeekly, 20.00m, now, new DateTime(yrStart, moStart, 17)));

            forecast.AddItem(new ForecastItem("Fuel", EForecastItemType.Expense, EFrequency.Weekly, 20.00m, now, new DateTime(yrStart, moStart, 23)));
            forecast.AddItem(new ForecastItem("Smokes", EForecastItemType.Expense, EFrequency.BiWeekly, 80.00m, now, new DateTime(yrStart, moStart, 20)));
            //forecast.AddItem(new ForecastItem("Cash", EForecastItemType.Expense, EFrequency.BiWeekly, 100.00m, now, new DateTime(yrStart, moStart, 25)));

            //cffSvc.Save(forecast);
            //Assert.That(forecast.Id.HasValue);

            // stick it in the engine and process it
            if (!(_engine.CreateForecast(_helper, forecast) is ForecastResult result))
                return;

            result.ForecastId = forecast.Id;

            //cffSvc.SaveForecastResult(result);

            PrintForecastResult(yrStart, moStart, dyStart, forecast, result);
            //ExportToExcel(yrStart, moStart, dyStart, forecast, result);
        }

        private static void PrintForecastResult(int yrStart, int moStart, int dyStart, Forecast forecast, ForecastResult result)
        {
            var idx = new DateTime(yrStart, moStart, dyStart);
            var lowBalances = new Dictionary<DateTime, decimal>();
            var currentMonth = moStart;

            var lowBalance = 999999.00m;
            var lowBalanceDate = DateTime.MaxValue;

            var output = new StringBuilder();

            output.AppendLine("***** RESULTS *****");
            while (idx <= forecast.End)
            {
                // did the month change?
                if (currentMonth != idx.Month)
                {
                    output.AppendLine($"Month changed from {currentMonth} to {idx.Month}");
                    // write the low balance for the previous month ...
                    lowBalances.Add(lowBalanceDate, lowBalance);
                    output.AppendLine($"\tAdded {lowBalanceDate} to {lowBalance}");
                    // then reset the lowBalance for the current month
                    lowBalance = 999999.00m;
                    lowBalanceDate = DateTime.MaxValue;
                    currentMonth = idx.Month;
                }

                var dailyBalance = result.Results.First(r => r.TransactionDate == idx).AmountEnd;

                if (dailyBalance < lowBalance)
                {
                    lowBalance = dailyBalance;
                    lowBalanceDate = idx;
                }

                output.AppendFormat("\t{0:dd-MMM-yyyy}: {1:C}", idx, dailyBalance).AppendLine();
                idx = idx.AddDays(1);
            }
            output.AppendLine("***** END RESULTS *****");
            output.AppendLine("");
            output.AppendLine("***** LOW BALANCES *****");
            // now i want to see the low balance for each month
            foreach (var kvp in lowBalances)
            {
                output.AppendFormat("\t{0:dd-MMM-yyyy}: {1:C}", kvp.Key, kvp.Value).AppendLine();
            }
            output.AppendLine("***** END LOW BALANCES *****");

            Console.WriteLine(output.ToString());
        }

        private void ExportToExcel(int yrStart, int moStart, int dyStart, Forecast forecast, ForecastResult result)
        {
            // Set the file name and get the output directory
            var fileName = "CFF_Report_" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            var outputDir = @"C:\\";

            // Create the file using the FileInfo object
            var file = new FileInfo(outputDir + fileName);

            // Create the package and make sure you wrap it in a using statement
            using (var package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Cash Flow Forecast - " + DateTime.Now.ToShortDateString());

                // --------- Data and styling goes here -------------- //
                // Add some formatting to the worksheet
                worksheet.TabColor = System.Drawing.Color.Blue;
                worksheet.DefaultRowHeight = 12;
                worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
                worksheet.Row(1).Height = 20;
                worksheet.Row(2).Height = 18;

                // Start adding the header
                // First of all the first row
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Date";
                worksheet.Cells[1, 3].Value = "Amount";

                worksheet.Column(2).Style.Numberformat.Format = "yyyy-mm-dd";
                worksheet.Column(3).Style.Numberformat.Format = "$###,###,##0.00";

                var idxRow = 2;

                var idx = new DateTime(yrStart, moStart, dyStart);

                Console.WriteLine("***** RESULTS *****");
                while (idx < forecast.End)
                {
                    var day = result.Results.FirstOrDefault(r => r.TransactionDate == idx);

                    if (day.Transactions.Count == 0)
                    {
                        idx = idx.AddDays(1);
                        continue;
                    }

                    foreach (var transaction in day.Transactions)
                    {
                        worksheet.Cells[idxRow, 1].Value = day.TransactionDate;
                        worksheet.Cells[idxRow, 2].Value = idx;
                        worksheet.Cells[idxRow, 3].Value = Convert.ToDecimal(transaction.Amount);
                        idxRow++;
                    }

                    idx = idx.AddDays(1);
                }

                // Set some document properties
                package.Workbook.Properties.Title = "Cash Flow Forecast";
                package.Workbook.Properties.Author = "jrandallsexton@gmail.com";
                package.Workbook.Properties.Company = "J. Randall Sexton";

                // save our new workbook and we are done!
                package.Save();
            }

            Process.Start(Path.Combine(outputDir, fileName));
        }

        /// <summary>
        /// This test is to ensure that forecast engines ignore events that should have already occured
        /// i.e. If I start a forecast on the 15th, it should not process, for example, a mortgage payment that was on the 5th
        /// </summary>
        [Test]
        public void Forecast_Processes_InMonth_Items_Correctly()
        {
            // set-up
            var now = DateTime.Now;

            var yrStart = now.Year;
            var moStart = now.Month;
            const int dyStart = 15;

            const decimal balanceStart = 500.00m;

            const int duration = 60;

            // Create the forecast
            var forecast = new Forecast("Forecast_Processes_InMonth_Items_Correctly", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration)
            {
                AmountBegin = balanceStart
            };

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 2000.00m, new DateTime(yrStart, moStart, 22), DateTime.Now));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 1000.00m, new DateTime(yrStart, moStart, 5), DateTime.Now));

            // stick it in the engine and process it
            var result = this._engine.CreateForecast(this._helper, forecast);

            var idx = new DateTime(yrStart, moStart, dyStart);

            while (idx < forecast.End)
            {
                Console.WriteLine("{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results.First(r => r.TransactionDate == idx).AmountEnd);
                idx = idx.AddDays(1);
            }

            Assert.That(result.AmountEnd == 14500.00m);
        }

        [TearDown]
        public void TearDown()
        {
            _helper = null;
            _engine = null;
        }

    }
}
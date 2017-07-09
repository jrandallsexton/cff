
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CFF.Data;
using CFF.Engines;
using CFF.Enumerations;
using CFF.Interfaces;
using CFF.Services;
using NUnit.Framework;
using OfficeOpenXml;

namespace CFF.Tests.Misc
{
    
    /// <summary>
    /// Note:  The methods in here are for experimentation - not actual unit testing
    /// The NUnit framework is simply here as a way to easily execute the code
    /// </summary>
    [TestFixture]
    public class TestCases
    {

        private IForecastHelper _helper = null;
        private IForecastEngine _engine = null;

        [TestFixtureSetUp]
        public void CreateHelper()
        {
            this._helper = new ForecastHelper();
            this._engine = new RoughEngine();
            this._engine.IsVerbose(true);
        }

        [Test]
        public void GetExistingForecast()
        {
            var cffSvc = new CffService(new CffContext("cffDb"));

            var forecast = cffSvc.Get(1);
            Assert.NotNull(forecast);
        }

        [Test]
        public void ProcessExistingForecastModel()
        {
            var cffSvc = new CffService(new CffContext("cffDb"));

            var forecast = cffSvc.Get(1);
            Assert.NotNull(forecast);

            // stick it in the engine and process it
            var result = this._engine.CreateForecast(this._helper, forecast) as ForecastResult;
            result.ForecastId = forecast.Id.Value;

            cffSvc.SaveForecastResult(result);

            PrintForecastResult(forecast.Begin.Year, forecast.Begin.Month, forecast.Begin.Day, forecast, result);
        }

        private static void PrintForecastResult(int yrStart, int moStart, int dyStart, IForecast forecast, IForecastResult result)
        {
            var idx = new DateTime(yrStart, moStart, dyStart);
            var lowBalances = new Dictionary<DateTime, decimal>();
            var currentMonth = moStart;

            var lowBalance = 999999.00m;
            var lowBalanceDate = DateTime.MaxValue;

            Console.WriteLine("***** RESULTS *****");
            while (idx < forecast.End)
            {
                // did the month change?
                if (currentMonth != idx.Month)
                {
                    //Console.WriteLine($"Month changed from {currentMonth} to {idx.Month}");
                    // write the low balance for the previous month ...
                    lowBalances.Add(lowBalanceDate, lowBalance);
                    //Console.WriteLine($"\tAdded {lowBalanceDate} to {lowBalance}");
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

                Console.WriteLine("\t{0:dd-MMM-yyyy}: {1:C}", idx, dailyBalance);
                idx = idx.AddDays(1);
            }
            Console.WriteLine("***** END RESULTS *****");
            Console.WriteLine("");
            Console.WriteLine("***** LOW BALANCES *****");
            // now i want to see the low balance for each month
            foreach (var kvp in lowBalances)
            {
                Console.WriteLine("\t{0}: {1:C}", kvp.Key.ToString("dd-MMM-yyyy"), kvp.Value);
            }
            Console.WriteLine("***** END LOW BALANCES *****");
        }

        [Test]
        public void TestCaseOne()
        {
            var cffSvc = new CffService(new CffContext("cffDb"));

            // set-up
            var now = DateTime.Now;

            var yrStart = now.Year;
            var moStart = now.Month;
            var dyStart = now.Day;

            const decimal balanceStart = 3344.30m;

            const int duration = 43;
             
            // Create the forecast
            var forecast = new Forecast("Test Case 2", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration)
            {
                AmountBegin = balanceStart
            };

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.BiWeekly, 3050.00m, now, new DateTime(yrStart, moStart, 7)));
            forecast.AddItem(new ForecastItem("Rental - Income", EForecastItemType.Income, EFrequency.Monthly, 2000.00m, 5));
            forecast.AddItem(new ForecastItem("Daycare Reimbursement", EForecastItemType.Income, EFrequency.BiWeekly, 150.00m, now, new DateTime(yrStart, moStart - 1, 30)));

            forecast.AddItem(new ForecastItem("Rent", EForecastItemType.Expense, EFrequency.Monthly, 1825.00m, 4));
            forecast.AddItem(new ForecastItem("Trustee", EForecastItemType.Expense, EFrequency.Monthly, 3370.00m, new DateTime(yrStart, 8, 17), null));

            // The following 3 items will drop off by 31 July
            forecast.AddItem(new ForecastItem("WF - Jun", EForecastItemType.Expense, EFrequency.Intermittent, 1555.00m, 10));
            forecast.AddItem(new ForecastItem("WF - Jul", EForecastItemType.Expense, EFrequency.Intermittent, 1555.00m, 24));

            forecast.AddItem(new ForecastItem("Electric", EForecastItemType.Expense, EFrequency.Monthly, 300.00m, 6));
            forecast.AddItem(new ForecastItem("Water", EForecastItemType.Expense, EFrequency.Monthly, 200.00m, 22));
            forecast.AddItem(new ForecastItem("Internet", EForecastItemType.Expense, EFrequency.Monthly, 100.00m, 28));
            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 260.00m, 18));
            forecast.AddItem(new ForecastItem("Geico", EForecastItemType.Expense, EFrequency.Monthly, 85.00m, 10));
            forecast.AddItem(new ForecastItem("Daycare", EForecastItemType.Expense, EFrequency.BiWeekly, 250.00m, now, new DateTime(yrStart, moStart, 7)));

            forecast.AddItem(new ForecastItem("Rental - Lawn", EForecastItemType.Expense, EFrequency.Monthly, 180.00m, now, new DateTime(yrStart, moStart, 8)));
            forecast.AddItem(new ForecastItem("Rental - Pool", EForecastItemType.Expense, EFrequency.Monthly, 245.00m, now, new DateTime(yrStart, 10, 7), new DateTime(yrStart, 7, 7)));
            forecast.AddItem(new ForecastItem("Rental - ADT", EForecastItemType.Expense, EFrequency.Monthly, 42.62m, 29));

            forecast.AddItem(new ForecastItem("Netflix", EForecastItemType.Expense, EFrequency.Monthly, 12.90m, 16));
            forecast.AddItem(new ForecastItem("Dropbox", EForecastItemType.Expense, EFrequency.Monthly, 5.00m, 5));
            forecast.AddItem(new ForecastItem("Apple Storage", EForecastItemType.Expense, EFrequency.Monthly, 5.00m, 7));
            forecast.AddItem(new ForecastItem("Google Drive", EForecastItemType.Expense, EFrequency.Monthly, 1.99m, 5));

            forecast.AddItem(new ForecastItem("Barbershop", EForecastItemType.Expense, EFrequency.BiWeekly, 20.00m, now, new DateTime(yrStart, 6, 24)));

            forecast.AddItem(new ForecastItem("Fuel", EForecastItemType.Expense, EFrequency.Weekly, 20.00m, now, new DateTime(yrStart, moStart, 8)));
            forecast.AddItem(new ForecastItem("Smokes", EForecastItemType.Expense, EFrequency.BiWeekly, 80.00m, now, new DateTime(yrStart, moStart, 9)));
            forecast.AddItem(new ForecastItem("Cash", EForecastItemType.Expense, EFrequency.BiWeekly, 100.00m, now, new DateTime(yrStart, moStart, 7)));

            cffSvc.Save(forecast);
            Assert.That(forecast.Id.HasValue);

            // stick it in the engine and process it
            var result = this._engine.CreateForecast(this._helper, forecast);

            PrintForecastResult(yrStart, moStart, dyStart, forecast, result);

        }

        private void ExportToExcel(int yrStart, int moStart, int dyStart, IForecast forecast, IForecastResult result)
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

        [TestFixtureTearDown]
        public void TearDown()
        {
            this._helper = null;
            this._engine = null;
        }

    }

}
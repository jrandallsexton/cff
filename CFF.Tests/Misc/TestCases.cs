
using System;

using CFF.Engines;
using CFF.Enumerations;
using CFF.Interfaces;

using NUnit.Framework;

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
        public void TestCaseOne()
        {

            // set-up
            var now = DateTime.Now;

            var yrStart = now.Year;
            var moStart = now.Month;
            var dyStart = now.Day;

            const decimal balanceStart = 828.00m;

            const int duration = 45;
             
            // Create the forecast
            var forecast = new Forecast("Test Case One", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration)
            {
                AmountBegin = balanceStart
            };

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.BiWeekly, 3050.00m, now, new DateTime(yrStart, moStart - 1, 23)));
            forecast.AddItem(new ForecastItem("Rental - Income", EForecastItemType.Income, EFrequency.Monthly, 2000.00m, 5));
            forecast.AddItem(new ForecastItem("Daycare Reimbursement", EForecastItemType.Income, EFrequency.BiWeekly, 150.00m, now, new DateTime(yrStart, moStart - 1, 30)));

            forecast.AddItem(new ForecastItem("Rent", EForecastItemType.Expense, EFrequency.Monthly, 1825.00m, 4));
            forecast.AddItem(new ForecastItem("Trustee", EForecastItemType.Expense, EFrequency.Monthly, 3370.00m, new DateTime(yrStart, 8, 17), null));

            // The following 3 items will drop off by 31 July
            forecast.AddItem(new ForecastItem("WF - Jun", EForecastItemType.Expense, EFrequency.Intermittent, 1555.00m, 10));
            forecast.AddItem(new ForecastItem("WF - Jul", EForecastItemType.Expense, EFrequency.Intermittent, 1555.00m, 24));

            forecast.AddItem(new ForecastItem("Electric", EForecastItemType.Expense, EFrequency.Monthly, 300.00m, 6));
            forecast.AddItem(new ForecastItem("Water", EForecastItemType.Expense, EFrequency.Monthly, 150.00m, 22));
            forecast.AddItem(new ForecastItem("Internet", EForecastItemType.Expense, EFrequency.Monthly, 100.00m, 28));
            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 260.00m, 18));
            forecast.AddItem(new ForecastItem("Geico", EForecastItemType.Expense, EFrequency.Monthly, 85.00m, 10));
            forecast.AddItem(new ForecastItem("Daycare", EForecastItemType.Expense, EFrequency.BiWeekly, 250.00m, now, new DateTime(yrStart, moStart - 1, 23)));

            forecast.AddItem(new ForecastItem("Rental - Lawn", EForecastItemType.Expense, EFrequency.Monthly, 180.00m, 7));
            forecast.AddItem(new ForecastItem("Rental - Pool", EForecastItemType.Expense, EFrequency.Monthly, 245.00m, 6));
            //forecast.AddItem(new ForecastItem("Rental - Pool", EForecastItemType.Expense, EFrequency.Monthly, 245.00m, now, new DateTime(yrStart, 9, 7), new DateTime(yrStart, 6, 7)));
            forecast.AddItem(new ForecastItem("Rental - ADT", EForecastItemType.Expense, EFrequency.Monthly, 42.62m, 29));

            forecast.AddItem(new ForecastItem("Netflix", EForecastItemType.Expense, EFrequency.Monthly, 12.90m, 16));
            forecast.AddItem(new ForecastItem("Dropbox", EForecastItemType.Expense, EFrequency.Monthly, 5.00m, 5));
            forecast.AddItem(new ForecastItem("Apple Storage", EForecastItemType.Expense, EFrequency.Monthly, 5.00m, 7));
            forecast.AddItem(new ForecastItem("Google Drive", EForecastItemType.Expense, EFrequency.Monthly, 1.99m, 5));

            forecast.AddItem(new ForecastItem("Barbershop", EForecastItemType.Expense, EFrequency.BiWeekly, 20.00m, now, new DateTime(yrStart, 6, 24)));

            forecast.AddItem(new ForecastItem("Fuel", EForecastItemType.Expense, EFrequency.Weekly, 20.00m, now, new DateTime(yrStart, moStart, 8)));
            forecast.AddItem(new ForecastItem("Smokes", EForecastItemType.Expense, EFrequency.BiWeekly, 80.00m, now, new DateTime(yrStart, moStart, 8)));
            forecast.AddItem(new ForecastItem("Cash", EForecastItemType.Expense, EFrequency.BiWeekly, 100.00m, now, new DateTime(yrStart, moStart, 7)));

            // stick it in the engine and process it
            var result = this._engine.CreateForecast(this._helper, forecast);

            var idx = new DateTime(yrStart, moStart, dyStart);

            Console.WriteLine("***** RESULTS *****");
            while (idx < forecast.End)
            {
                Console.WriteLine("\t{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results[idx].AmountEnd);
                //if ((idx.Day%15 == 0) || (result.Results[idx].AmountEnd < 0))
                //{
                //    Console.WriteLine("{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results[idx].AmountEnd);
                //}
                idx = idx.AddDays(1);
            }
            Console.WriteLine("***** END RESULTS *****");

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
                Console.WriteLine("{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results[idx].AmountEnd);
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
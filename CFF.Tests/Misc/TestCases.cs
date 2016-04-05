
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
        }

        [Test]
        public void TestCaseOne()
        {

            // set-up
            var now = DateTime.Now;

            var yrStart = now.Year;
            var moStart = now.Month;
            var dyStart = now.Day;

            const decimal balanceStart = 200.00m;

            const int duration = 90;
             
            // Create the forecast
            var forecast = new Forecast("Test Case One", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration)
            {
                AmountBegin = balanceStart
            };

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.BiWeekly, 3275.00m, 15));
            forecast.AddItem(new ForecastItem("Rental", EForecastItemType.Income, EFrequency.Monthly, 2000.00m, 15));

            forecast.AddItem(new ForecastItem("Rent", EForecastItemType.Expense, EFrequency.Monthly, 1725.00m, 1));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 1585.00m, 15));

            forecast.AddItem(new ForecastItem("Electric", EForecastItemType.Expense, EFrequency.Monthly, 300.00m, 15));
            forecast.AddItem(new ForecastItem("Water", EForecastItemType.Expense, EFrequency.Monthly, 150.00m, 15));
            forecast.AddItem(new ForecastItem("Student Loan", EForecastItemType.Expense, EFrequency.Monthly, 103.87m, 28));
            forecast.AddItem(new ForecastItem("Prosper", EForecastItemType.Expense, EFrequency.Monthly, 559.01m, 4));
            forecast.AddItem(new ForecastItem("Lending Club", EForecastItemType.Expense, EFrequency.Monthly, 875.39m, 20));
            forecast.AddItem(new ForecastItem("Internet", EForecastItemType.Expense, EFrequency.Monthly, 60.00m, 28));

            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 145.00m, 18));
            forecast.AddItem(new ForecastItem("Skype", EForecastItemType.Expense, EFrequency.Monthly, 9.00m, 15));
            forecast.AddItem(new ForecastItem("ADT", EForecastItemType.Expense, EFrequency.Monthly, 45.00m, 29));
            forecast.AddItem(new ForecastItem("Groceries", EForecastItemType.Expense, EFrequency.Weekly, 50.00m, 6));

            forecast.AddItem(new ForecastItem("Netflix", EForecastItemType.Expense, EFrequency.Monthly, 9.00m, 29));
            forecast.AddItem(new ForecastItem("Fuel", EForecastItemType.Expense, EFrequency.Weekly, 40.00m, 3));

            //forecast.AddItem(new ForecastItem("Laban", EForecastItemType.Expense, EFrequency.Intermittent, 450.00m, 1));

            forecast.AddItem(new ForecastItem("CC - BoA", EForecastItemType.Expense, EFrequency.Monthly, 68.00m, 24));
            forecast.AddItem(new ForecastItem("CC - BN", EForecastItemType.Expense, EFrequency.Monthly, 132.00m, 27));
            forecast.AddItem(new ForecastItem("CC - ML", EForecastItemType.Expense, EFrequency.Monthly, 158.00m, 23));
            forecast.AddItem(new ForecastItem("CC - Amex", EForecastItemType.Expense, EFrequency.Monthly, 116.00m, 11));
            forecast.AddItem(new ForecastItem("CC - USBank", EForecastItemType.Expense, EFrequency.Monthly, 30.00m, 19));
            forecast.AddItem(new ForecastItem("CC - PayPal", EForecastItemType.Expense, EFrequency.Monthly, 35.00m, 19));
            forecast.AddItem(new ForecastItem("CC - CapOne", EForecastItemType.Expense, EFrequency.Monthly, 56.00m, 22));
            forecast.AddItem(new ForecastItem("CC - BestBuy", EForecastItemType.Expense, EFrequency.Monthly, 25.00m, 5));
            forecast.AddItem(new ForecastItem("CC - Discover", EForecastItemType.Expense, EFrequency.Monthly, 180.00m, 1));
            forecast.AddItem(new ForecastItem("CC - HHonors", EForecastItemType.Expense, EFrequency.Monthly, 41.00m, 10));
            forecast.AddItem(new ForecastItem("CC - Citi Simpl", EForecastItemType.Expense, EFrequency.Monthly, 43.00m, 6));
            forecast.AddItem(new ForecastItem("CC - Chase", EForecastItemType.Expense, EFrequency.Monthly, 99.00m, 4));
            forecast.AddItem(new ForecastItem("CC - Amazon", EForecastItemType.Expense, EFrequency.Monthly, 84.00m, 12));

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
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 2000.00m, new DateTime(yrStart, moStart, 22)));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 1000.00m, new DateTime(yrStart, moStart, 5)));

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
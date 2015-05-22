
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            const decimal balanceStart = 400.00m;

            const int duration = 90;
             
            // Create the forecast
            var forecast = new Forecast("Test Case One", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration)
            {
                AmountBegin = balanceStart
            };

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 1615.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Rent", EForecastItemType.Income, EFrequency.Monthly, 1900.00m, new DateTime(yrStart, moStart, 1)));
            forecast.AddItem(new ForecastItem("Verandah", EForecastItemType.Expense, EFrequency.Monthly, 1085.00m, new DateTime(yrStart, moStart, 3)));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 1585.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Student Loan", EForecastItemType.Expense, EFrequency.Monthly, 103.87m, new DateTime(yrStart, moStart, 28)));
            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 145.00m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Credit Cards", EForecastItemType.Expense, EFrequency.Monthly, 900.00m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Prosper", EForecastItemType.Expense, EFrequency.Monthly, 559.01m, new DateTime(yrStart, moStart, 4)));
            forecast.AddItem(new ForecastItem("Lending Club", EForecastItemType.Expense, EFrequency.Monthly, 875.39m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Internet", EForecastItemType.Expense, EFrequency.Monthly, 7.00m, new DateTime(yrStart, moStart, 28)));
            forecast.AddItem(new ForecastItem("Skype", EForecastItemType.Expense, EFrequency.Monthly, 8.52m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Electric", EForecastItemType.Expense, EFrequency.Monthly, 200.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Water", EForecastItemType.Expense, EFrequency.Monthly, 100.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Groceries", EForecastItemType.Expense, EFrequency.Weekly, 150.00m, new DateTime(yrStart, moStart, 11)));

            // stick it in the engine and process it
            var result = this._engine.CreateForecast(this._helper, forecast);

            var idx = new DateTime(yrStart, moStart, dyStart);

            while (idx < forecast.End)
            {
                if (idx.Day%15 == 0) { Console.WriteLine("{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results[idx].AmountEnd); }
                idx = idx.AddDays(1);
            }

            
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
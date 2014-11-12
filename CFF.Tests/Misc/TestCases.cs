
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
            DateTime now = DateTime.Now;

            int yrStart = now.Year;
            int moStart = now.Month;
            int dyStart = now.Day;

            decimal balanceStart = 1400.00m;

            int duration = 365;
             
            // Create the forecast
            Forecast forecast = new Forecast("Test Case One", EForecastType.Snapshot, new DateTime(yrStart, moStart, dyStart), duration);
            forecast.AmountBegin = balanceStart;

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 1728.00m, new DateTime(yrStart, moStart, 13))); //, new DateTime(2015, 1, 7)));
            forecast.AddItem(new ForecastItem("Verandah", EForecastItemType.Expense, EFrequency.Monthly, 1100.00m, new DateTime(yrStart, 12, 3)));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 1585.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Student Loan", EForecastItemType.Expense, EFrequency.Monthly, 103.87m, new DateTime(yrStart, moStart, 28)));
            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 145.00m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Credit Cards", EForecastItemType.Expense, EFrequency.Monthly, 900.00m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Car Note", EForecastItemType.Expense, EFrequency.Monthly, 732.50m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Prosper", EForecastItemType.Expense, EFrequency.Monthly, 559.01m, new DateTime(yrStart, 12, 4)));
            forecast.AddItem(new ForecastItem("Lending Club", EForecastItemType.Expense, EFrequency.Monthly, 875.39m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Internet", EForecastItemType.Expense, EFrequency.Monthly, 7.00m, new DateTime(yrStart, moStart, 28)));
            forecast.AddItem(new ForecastItem("Skype", EForecastItemType.Expense, EFrequency.Monthly, 8.52m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Electric - Mine", EForecastItemType.Expense, EFrequency.Monthly, 120.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Electric - Hers", EForecastItemType.Expense, EFrequency.Monthly, 150.00m, new DateTime(yrStart, moStart, 20), new DateTime(2015, 02, 15)));
            forecast.AddItem(new ForecastItem("Water - Mine", EForecastItemType.Expense, EFrequency.Monthly, 50.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Water - Hers", EForecastItemType.Expense, EFrequency.Monthly, 75.00m, new DateTime(yrStart, moStart, 20), new DateTime(2015, 02, 15)));

            // stick it in the engine and process it
            IForecastResult result = this._engine.CreateForecast(this._helper, forecast);

            DateTime idx = new DateTime(yrStart, moStart, dyStart);

            while (idx < forecast.End)
            {
                if (idx.Day == 30) { Console.WriteLine("{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results[idx].AmountEnd); }
                idx = idx.AddDays(1);
            }

            
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            this._helper = null;
            this._engine = null;
        }

    }

}
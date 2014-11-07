
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

        private ForecastHelper _helper = null;

        [TestFixtureSetUp]
        public void CreateHelper()
        {
            this._helper = new ForecastHelper();
        }

        [Test]
        public void TestCaseOne()
        {

            int yrStart = 2014;
            int moStart = 11;
            int duration = 90;

            // Create the forecast
            Forecast forecast = new Forecast("Test Case One", EForecastType.Snapshot, new DateTime(yrStart, moStart, 7), duration);
            forecast.AmountBegin = 1400m;

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 1728.00m, new DateTime(yrStart, moStart, 6)));
            forecast.AddItem(new ForecastItem("Wells Fargo", EForecastItemType.Expense, EFrequency.Monthly, 1585.00m, new DateTime(yrStart, moStart, 15)));
            forecast.AddItem(new ForecastItem("Verizon", EForecastItemType.Expense, EFrequency.Monthly, 145.00m, new DateTime(yrStart, moStart, 18)));
            forecast.AddItem(new ForecastItem("Car Note", EForecastItemType.Expense, EFrequency.Monthly, 732.50m, new DateTime(yrStart, moStart, 15)));

            // stick it in the engine and process it
            RoughEngine engine = new RoughEngine();
            IForecastResult result = engine.CreateForecast(this._helper, forecast);

            DateTime idx = new DateTime(yrStart, moStart, 7);

            while (idx < forecast.End)
            {
                Console.WriteLine("{0}: {1:C}", idx.ToString("dd-MMM-yyyy"), result.Results[idx].AmountEnd);
                idx = idx.AddDays(1);
            }

            
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            this._helper = null;
        }

    }

}
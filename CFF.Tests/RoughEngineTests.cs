
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using CFF;
using CFF.Enumerations;
using CFF.Interfaces;

using CFF.Engines;

namespace CFF.Tests
{

    [TestFixture]
    public class RoughEngineTests
    {

        [Test]
        public void Snapshot_Forecast_Models_Correctly_Over_Thirty_Days()
        {

            // Create the forecast
            Forecast forecast = new Forecast("Test CFF", EForecastType.Snapshot, new DateTime(2015, 1, 1), 30);

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, new Decimal(1000), new DateTime(2015, 1, 6)));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, new Decimal(850), new DateTime(2015, 1, 14)));
            forecast.AddItem(new ForecastItem("Car Note", EForecastItemType.Expense, EFrequency.Monthly, new Decimal(250), new DateTime(2015, 1, 28)));

            // stick it in the engine and process it
            RoughEngine engine = new RoughEngine();
            IForecastResult result = engine.CreateForecast(new ForecastHelper(), forecast);

            Assert.AreEqual(forecast.AmountBegin, result.AmountBegin);
            Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 1)].AmountEnd);
            Assert.AreEqual(new Decimal(3500), result.AmountEnd);

        }

    }

}
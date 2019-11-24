
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

using CFF.Tests.Factories;
using CFF.Tests.Interfaces;
using CFF.Tests.Mocks;

namespace CFF.Tests
{

    [TestFixture]
    public class RoughEngineTests
    {

        private RoughEngine _engine = null;
        private IForecastFactory _factory = null;
        private IForecastHelper _helper = null;

        [TestFixtureSetUp]
        public void Setup()
        {
            this._engine = new RoughEngine();
            this._helper = new ForecastHelper();
            this._factory = new ForecastFactory();
        }

        //[Test]
        //public void Snapshot_Forecast_Models_Correctly_Over_Sixty_Days()
        //{

        //    // Create the forecast
        //    IForecast forecast = this._factory.Create();

        //    // stick it in the engine and process it
        //    IForecastResult result = this._engine.CreateForecast(new ForecastHelper(), forecast);

        //    Assert.AreEqual(forecast.AmountBegin, result.AmountBegin);

        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 1)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 2)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 3)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 4)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 5)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 6)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 7)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 8)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 9)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 10)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 11)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 12)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3000), result.Results[new DateTime(2015, 1, 13)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 14)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 15)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 16)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 17)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 18)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 19)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 20)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 21)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 22)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 23)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 24)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 25)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 26)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4015), result.Results[new DateTime(2015, 1, 27)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 28)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 29)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 30)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 31)].AmountEnd);

        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 2, 1)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 2, 2)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 3)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 4)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 5)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 6)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 7)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 8)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 9)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 10)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 11)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 12)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 13)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4915), result.Results[new DateTime(2015, 2, 14)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4915), result.Results[new DateTime(2015, 2, 15)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4915), result.Results[new DateTime(2015, 2, 16)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5915), result.Results[new DateTime(2015, 2, 17)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5915), result.Results[new DateTime(2015, 2, 18)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5915), result.Results[new DateTime(2015, 2, 19)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 20)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 21)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 22)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 23)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 24)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 25)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 26)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 27)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6530), result.Results[new DateTime(2015, 2, 28)].AmountEnd);

        //    Assert.AreEqual(new Decimal(6530), result.AmountEnd);

        //}

        //[Test]
        //public void Snapshot_Forecast_Models_Correctly_Over_Sixty_Days_With_Expiring_Item()
        //{

        //    // Create the forecast
        //    IForecast forecast = this._factory.CreateWithExpiringItem();

        //    // stick it in the engine and process it
        //    IForecastResult result = this._engine.CreateForecast(new ForecastHelper(), forecast);

        //    Assert.AreEqual(forecast.AmountBegin, result.AmountBegin);

        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 1)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 2)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 3)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 4)].AmountEnd);
        //    Assert.AreEqual(new Decimal(1000), result.Results[new DateTime(2015, 1, 5)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 6)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 7)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 8)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 9)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 10)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 11)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2000), result.Results[new DateTime(2015, 1, 12)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3000), result.Results[new DateTime(2015, 1, 13)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 14)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 15)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 16)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 17)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 18)].AmountEnd);
        //    Assert.AreEqual(new Decimal(2150), result.Results[new DateTime(2015, 1, 19)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 20)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 21)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 22)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 23)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 24)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 25)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3015), result.Results[new DateTime(2015, 1, 26)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4015), result.Results[new DateTime(2015, 1, 27)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 28)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 29)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 30)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 1, 31)].AmountEnd);

        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 2, 1)].AmountEnd);
        //    Assert.AreEqual(new Decimal(3765), result.Results[new DateTime(2015, 2, 2)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 3)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 4)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 5)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 6)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 7)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 8)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4765), result.Results[new DateTime(2015, 2, 9)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 10)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 11)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 12)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5765), result.Results[new DateTime(2015, 2, 13)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4915), result.Results[new DateTime(2015, 2, 14)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4915), result.Results[new DateTime(2015, 2, 15)].AmountEnd);
        //    Assert.AreEqual(new Decimal(4915), result.Results[new DateTime(2015, 2, 16)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5915), result.Results[new DateTime(2015, 2, 17)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5915), result.Results[new DateTime(2015, 2, 18)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5915), result.Results[new DateTime(2015, 2, 19)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 20)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 21)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 22)].AmountEnd);
        //    Assert.AreEqual(new Decimal(5780), result.Results[new DateTime(2015, 2, 23)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 24)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 25)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 26)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 27)].AmountEnd);
        //    Assert.AreEqual(new Decimal(6780), result.Results[new DateTime(2015, 2, 28)].AmountEnd);

        //    Assert.AreEqual(new Decimal(6780), result.AmountEnd);

        //}

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Engine_Throws_Exception_With_Null_ForecastHelper()
        {
            this._engine.CreateForecast(null, this._factory.Create());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Engine_Throws_Exception_With_Null_Forecast()
        {
            this._engine.CreateForecast(this._helper, null);
        }

        [Test]
        public void ProcessRevolvingAcct()
        {
            var rev = new ForecastRevAccount
            {
                InitialAmount = 5000.00m,
                InterestRate = 00.189m,
                PaymentPercent = 0.04m,
                MinimumPayment = 15.00m
            };

            this._engine.ProcessRevolving(rev);

        }
        [TestFixtureTearDown]
        public void TearDown()
        {
            this._helper = null;
            this._factory = null;
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF;
using CFF.Enumerations;
using CFF.Interfaces;

using CFF.Tests.Interfaces;
using CFF.Tests.Factories;

using NUnit.Framework;

namespace CFF.Tests
{

    [TestFixture]
    public class ForecastHelperTests
    {

        private IForecastHelper _helper = null;
        private IForecastFactory _factory = null;

        [TestFixtureSetUp]
        public void CreateHelper()
        {
            this._helper = new ForecastHelper();
            this._factory = new ForecastFactory();
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_All_Frequencies()
        {
            foreach (EFrequency val in Enum.GetValues(typeof(EFrequency)))
            {
                this._helper.GetDueDate(new DateTime(2015, 1, 1), val);
            }
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Daily()
        {
            Assert.AreEqual(new DateTime(2015, 1, 2), this._helper.GetDueDate(new DateTime(2015, 1, 1), EFrequency.Daily));
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Weekly_SameMonth()
        {
            Assert.AreEqual(new DateTime(2015, 1, 8), this._helper.GetDueDate(new DateTime(2015, 1, 1), EFrequency.Weekly));
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Weekly_DifferentMonth()
        {
            Assert.AreEqual(new DateTime(2015, 2, 5), this._helper.GetDueDate(new DateTime(2015, 1, 29), EFrequency.Weekly));
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Monthly()
        {
            Assert.AreEqual(new DateTime(2015, 3, 15), this._helper.GetDueDate(new DateTime(2015, 2, 15), EFrequency.Monthly));
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Quarterly()
        {
            Assert.AreEqual(new DateTime(2015, 4, 15), this._helper.GetDueDate(new DateTime(2015, 1, 15), EFrequency.Quarterly));
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Annually()
        {
            Assert.AreEqual(new DateTime(2016, 4, 15), this._helper.GetDueDate(new DateTime(2015, 4, 15), EFrequency.Annually));
        }

        [Test]
        public void ForecastHelper_Determines_All_Due_Dates()
        {
            // Create the forecast
            IForecast forecast = this._factory.Create();

            IDictionary<DateTime, IList<IForecastItem>> values = this._helper.GenerateDueDates(forecast);

            Assert.AreEqual(0, values[new DateTime(2015, 1, 1)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 2)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 3)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 4)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 5)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 1, 6)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 7)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 8)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 9)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 10)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 11)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 12)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 1, 13)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 1, 14)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 15)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 16)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 17)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 18)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 19)].Count);
            Assert.AreEqual(2, values[new DateTime(2015, 1, 20)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 21)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 22)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 23)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 24)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 25)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 26)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 1, 27)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 1, 28)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 29)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 30)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 1, 31)].Count);

            Assert.AreEqual(0, values[new DateTime(2015, 2, 1)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 2)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 3)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 4)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 5)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 6)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 7)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 8)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 9)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 10)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 11)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 12)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 13)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 14)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 15)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 16)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 17)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 18)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 19)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 20)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 21)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 22)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 23)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 24)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 25)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 26)].Count);
            Assert.AreEqual(0, values[new DateTime(2015, 2, 27)].Count);
            Assert.AreEqual(1, values[new DateTime(2015, 2, 28)].Count);

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            this._helper = null;
            this._factory = null;
        }

    }

}
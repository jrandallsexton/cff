
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF;
using CFF.Enumerations;

using NUnit.Framework;

namespace CFF.Tests
{

    [TestFixture]
    public class ForecastHelperTests
    {

        private ForecastHelper _helper = null;

        [TestFixtureSetUp]
        public void CreateHelper()
        {
            this._helper = new ForecastHelper();
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

    }

}
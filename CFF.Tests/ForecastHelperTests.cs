
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
            Assert.Fail();
        }

        [Test]
        public void ForecastHelper_Calculates_DueDate_From_Frequency_Daily()
        {
            Assert.Fail();
        }

    }

}
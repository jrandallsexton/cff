
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Interfaces;

using NSubstitute;
using NUnit.Framework;

namespace CFF.Tests.Engines
{

    [TestFixture]
    public class EngineTests
    {

        [Test]
        public void Foo()
        {

            var forecast = Substitute.For<IForecast>();
            var sub = Substitute.For<IForecastEngine>();

            var result = sub.CreateForecast(new ForecastHelper(), forecast);

            Assert.NotNull(result);

        }

    }

}
using System;
using System.Collections.Generic;
using Cff.Core.Enumerations;
using Cff.Core.Interfaces;
using Cff.Core.Models;

namespace Cff.Core.Tests.Mocks
{

    public class MockForecastHelper : IForecastHelper 
    {

        public DateTime GetDueDate(DateTime lastProcessed, EFrequency frequency)
        {
            return new DateTime(2015, 1, 1);
        }

        public IDictionary<DateTime, IList<ForecastItem>> GenerateDueDates(Forecast forecast)
        {
            return new Dictionary<DateTime, IList<ForecastItem>>();
        }

    }
}
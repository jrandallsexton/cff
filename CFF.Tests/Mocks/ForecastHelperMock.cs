
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;
using CFF.Interfaces;

namespace CFF.Tests.Mocks
{

    public class ForecastHelperMock : IForecastHelper 
    {

        public DateTime GetDueDate(DateTime lastProcessed, EFrequency frequency)
        {
            return new DateTime(2015, 1, 1);
        }

        public IDictionary<DateTime, IList<IForecastItem>> GenerateDueDates(IForecast forecast)
        {
            return new Dictionary<DateTime, IList<IForecastItem>>();
        }

    }

}
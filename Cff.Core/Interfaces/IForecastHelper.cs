using System;
using System.Collections.Generic;
using System.Text;
using Cff.Core.Enumerations;
using Cff.Core.Models;

namespace Cff.Core.Interfaces
{
    public interface IForecastHelper
    {
        DateTime GetDueDate(DateTime lastProcessed, EFrequency frequency);
        IDictionary<DateTime, IList<ForecastItem>> GenerateDueDates(Forecast forecast);
    }
}

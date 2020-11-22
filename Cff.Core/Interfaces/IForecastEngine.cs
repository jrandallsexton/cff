using System;
using System.Collections.Generic;
using System.Text;
using Cff.Core.Implementations;
using Cff.Core.Models;

namespace Cff.Core.Interfaces
{
    public interface IForecastEngine
    {
        ForecastResult CreateForecast(IForecastHelper helper, Forecast forecast);
        void IsVerbose(bool isVerbose);
    }
}
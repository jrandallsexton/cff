
using Cff.Core.Implementations;
using Cff.Core.Interfaces;
using Cff.Core.Models;

using System;

namespace Cff.Core.Engines
{
    public class LinqEngine : IForecastEngine
    {
        private bool _verbose = false;

        public void IsVerbose(bool isVerbose)
        {
            this._verbose = isVerbose;
        }

        public ForecastResult CreateForecast(IForecastHelper helper, Forecast forecast)
        {
            throw new NotImplementedException();
        }

    }
}
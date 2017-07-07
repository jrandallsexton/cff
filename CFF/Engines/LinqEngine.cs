
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Interfaces;

namespace CFF.Engines
{

    public class LinqEngine : IForecastEngine 
    {
        private bool _verbose = false;
        public void IsVerbose(bool isVerbose)
        {
            this._verbose = isVerbose;
        }

        public IForecastResult CreateForecast(IForecastHelper helper, IForecast forecast)
        {
            throw new NotImplementedException();
        }

    }

}
﻿
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

        public IForecastResult CreateForecast(IForecastHelper helper, IForecast forecast)
        {
            throw new NotImplementedException();
        }

    }

}
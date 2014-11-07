
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Interfaces;

namespace CFF.Tests.Interfaces
{

    internal interface IForecastFactory
    {
        IForecast Create();
    }

}

using System;

namespace CFF.Interfaces
{

    public interface IForecastEngine
    {
        IForecastResult CreateForecast(IForecastHelper helper, IForecast forecast);
    }

}
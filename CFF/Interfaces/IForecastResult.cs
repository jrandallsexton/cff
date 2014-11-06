
using System;
using System.Collections.Generic;

namespace CFF.Interfaces
{

    public interface IForecastResult
    {
        Decimal AmountBegin { get; set; }
        Decimal AmountEnd { get; set; }
        IDictionary<DateTime, IForecastResultItem> Results { get; set; }
    }

}
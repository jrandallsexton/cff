
using System;
using System.Collections.Generic;

namespace CFF.Interfaces
{
    public interface IForecastResult
    {
        int? Id { get; set; }

        decimal AmountBegin { get; set; }

        decimal AmountEnd { get; set; }

        //IDictionary<DateTime, IForecastResultItem> Results { get; set; }

        ICollection<ForecastResultItem> Results { get; set; }

        DateTime Created { get; set; }
    }
}
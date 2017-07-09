﻿
using System;
using System.Collections.Generic;

using CFF.Enumerations;

namespace CFF.Interfaces
{

    public interface IForecast
    {
        int? Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        DateTime Created { get; set; }

        EForecastType ForecastType { get; set; }

        DateTime Begin { get; set; }

        DateTime End { get; set; }

        Decimal AmountBegin { get; set; }

        IEnumerable<ForecastItem> Items { get; }

        void AddItem(ForecastItem item);
    }

}
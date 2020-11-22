using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Cff.Core.Interfaces;
using Cff.Core.Models;

namespace Cff.Core.Implementations
{
    [NotMapped]
    public class ForecastItemWorkspace : ForecastItem
    {

        public DateTime LastProcessed { get; set; }
        public DateTime Due { get; set; }


        public ForecastItemWorkspace(ForecastItem forecastItem, DateTime lastProcessed, DateTime due)
        {
            this.Id = forecastItem.Id;
            this.UId = forecastItem.UId;
            this.Amount = forecastItem.Amount;
            this.Begin = forecastItem.Begin;
            this.End = forecastItem.End;
            this.Frequency = forecastItem.Frequency;
            this.Name = forecastItem.Name;
            this.Type = forecastItem.Type;
            this.LastProcessed = lastProcessed;
            this.Due = due;
        }

    }
}

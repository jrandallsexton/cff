
using System;
using System.Collections.Generic;

using CFF.Interfaces;

namespace CFF
{

    public class ForecastResult : IForecastResult 
    {
        public int? Id { get; set; }

        public int ForecastId { get; set; }

        public decimal AmountBegin { get; set; }

        public decimal AmountEnd { get; set; }

        //public IDictionary<DateTime, IForecastResultItem> Results { get; set; }
        public virtual ICollection<ForecastResultItem> Results { get; set; }

        public DateTime Created { get; set; }

        public ForecastResult(decimal amtBegin)
        {
            this.AmountBegin = amtBegin;
            //this.Results = new Dictionary<DateTime, IForecastResultItem>();
            this.Results = new List<ForecastResultItem>();
            this.Created = DateTime.UtcNow;
        }

    }

}
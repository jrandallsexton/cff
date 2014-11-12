
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Interfaces;

namespace CFF
{

    public class ForecastResult : IForecastResult 
    {

        public decimal AmountBegin { get; set; }

        public decimal AmountEnd { get; set; }

        public IDictionary<DateTime, IForecastResultItem> Results { get; set; }

        public ForecastResult(decimal amtBegin)
        {
            this.AmountBegin = amtBegin;
            this.Results = new Dictionary<DateTime, IForecastResultItem>();
        }

    }

}
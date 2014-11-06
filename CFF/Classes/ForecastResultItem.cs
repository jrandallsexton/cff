
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Interfaces;

namespace CFF
{

    public class ForecastResultItem : IForecastResultItem 
    {
        public decimal AmountBegin { get; set; }
        public decimal AmountEnd { get; set; }

        public ForecastResultItem(decimal amtBegin, decimal amtEnd)
        {
            this.AmountBegin = amtBegin;
            this.AmountEnd = amtEnd;
        }
    }

}
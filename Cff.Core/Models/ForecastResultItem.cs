using System;
using System.Collections.Generic;
using Cff.Core.Implementations;

namespace Cff.Core.Models
{
    public class ForecastResultItem : Entity<int>
    {
        public int ForecastResultId { get; set; }

        public decimal AmountBegin { get; set; }

        public decimal AmountEnd { get; set; }

        public DateTime TransactionDate { get; set; }

        //public Dictionary<string, decimal> Transactions { get; set; }

        public virtual ICollection<ForecastResultItemTransaction> Transactions { get; set; }

        public ForecastResultItem(decimal amtBegin, decimal amtEnd)
        {
            this.AmountBegin = amtBegin;
            this.AmountEnd = amtEnd;
            //this.Transactions = new Dictionary<string, decimal>();
            this.Transactions = new List<ForecastResultItemTransaction>();
        }
    }
}
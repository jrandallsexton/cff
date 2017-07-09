
using System;
using System.Collections.Generic;
using CFF.Implementations;

namespace CFF.Interfaces
{
    public interface IForecastResultItem
    {
        int? Id { get; set; }

        int ForecastResultId { get; set; }

        decimal AmountBegin { get; set; }

        decimal AmountEnd { get; set; }

        DateTime TransactionDate { get; set; }

        //Dictionary<string, decimal> Transactions { get; set; }

        ICollection<ForecastResultItemTransaction> Transactions { get; set; }
    }
}

using System.Collections.Generic;

namespace CFF.Interfaces
{

    public interface IForecastResultItem
    {
        decimal AmountBegin { get; set; }
        decimal AmountEnd { get; set; }
        Dictionary<string, decimal> Transactions { get; set; }
    }

}
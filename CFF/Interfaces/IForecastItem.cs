
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;

namespace CFF
{

    public interface IForecastItem
    {
        Guid Id { get; }
        string Name { get; set; }
        EForecastItemType Type { get; set; }
        EFrequency Frequency { get; set; }
        DateTime Begin { get; set; }
        DateTime End { get; set; }
        Decimal Amount { get; set; }
        EDurationType DurationType { get; set; }
        int DurationValue { get; set; }
        DateTime? LastProcessed { get; set; }
    }

}
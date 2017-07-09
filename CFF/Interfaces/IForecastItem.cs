
using System;

using CFF.Enumerations;

namespace CFF
{

    public interface IForecastItem
    {
        int Id { get; }

        Guid UId { get; set; }

        string Name { get; set; }

        EForecastItemType Type { get; set; }

        EFrequency Frequency { get; set; }

        DateTime Begin { get; set; }

        DateTime? End { get; set; }

        Decimal Amount { get; set; }

        EDurationType DurationType { get; set; }

        int DurationValue { get; set; }

        DateTime? LastProcessed { get; set; }

        DateTime Due { get; set; }
    }

}
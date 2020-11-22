
using System;

namespace Cff.Core.Implementations
{
    public class ForecastResultItemTransaction
    {
        public int? Id { get; set; }

        public Guid ForecastResultItemId { get; set; }

        public int ForecastItemId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
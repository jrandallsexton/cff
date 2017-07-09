using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFF.Implementations
{
    public class ForecastResultItemTransaction
    {
        public int? Id { get; set; }

        public int ForecastResultItemId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;

namespace CFF.Interfaces
{

    public interface IForecast
    {
        string Name { get; set; }
        string Description { get; set; }
        DateTime Created { get; set; }
        EForecastType Type { get; set; }
        DateTime Begin { get; set; }
        DateTime End { get; set; }
        Decimal AmountBegin { get; set; }
        IEnumerable<IForecastItem> Items { get; }

        void AddItem(IForecastItem item);
    }

}
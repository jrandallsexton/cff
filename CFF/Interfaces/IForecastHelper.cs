
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;

namespace CFF.Interfaces
{

    public interface IForecastHelper
    {
        DateTime GetDueDate(DateTime lastProcessed, EFrequency frequency);
    }

}
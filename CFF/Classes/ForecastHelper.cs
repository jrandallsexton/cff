
using CFF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;

namespace CFF
{

    public class ForecastHelper : IForecastHelper
    {

        public DateTime GetDueDate(DateTime lastProcessed, Enumerations.EFrequency frequency)
        {
            switch (frequency)
            {
                case EFrequency.Annually: return lastProcessed.AddYears(1);
                case EFrequency.BiAnnually: return lastProcessed.AddDays(180);
                case EFrequency.BiMonthly: return lastProcessed.AddMonths(2);
                case EFrequency.BiWeekly: return lastProcessed.AddDays(14);
                case EFrequency.Daily: return lastProcessed.AddDays(1);
                //case EFrequency.Intermittent
                case EFrequency.Monthly: return lastProcessed.AddMonths(1);
                case EFrequency.Quarterly: return lastProcessed.AddMonths(3);
                case EFrequency.Weekly: return lastProcessed.AddDays(7);
                default: return lastProcessed;
            }
        }
    }

}
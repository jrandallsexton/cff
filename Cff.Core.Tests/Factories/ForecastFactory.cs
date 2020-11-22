using Cff.Core.Enumerations;
using Cff.Core.Models;

using System;

namespace Cff.Core.Tests.Factories
{
    internal interface IForecastFactory
    {
        Forecast Create();
        Forecast CreateWithExpiringItem();
    }

    public class ForecastFactory : IForecastFactory 
    {

        public Forecast Create()
        {

            int yr = 2015;
            int duration = 60;

            // Create the forecast
            Forecast forecast = new Forecast("Test CFF", EForecastType.Snapshot, new DateTime(yr, 1, 1), duration);
            forecast.AmountBegin = 1000m;

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 1000m, new DateTime(yr, 1, 6), DateTime.Now));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 850m, new DateTime(yr, 1, 14), DateTime.Now));
            forecast.AddItem(new ForecastItem("Cell Phone", EForecastItemType.Expense, EFrequency.Monthly, 135m, new DateTime(yr, 1, 20), DateTime.Now));
            forecast.AddItem(new ForecastItem("Car Note", EForecastItemType.Expense, EFrequency.Monthly, 250m, new DateTime(yr, 1, 28), DateTime.Now));

            return forecast;
        }

        public Forecast CreateWithExpiringItem()
        {
            int yr = 2015;
            int duration = 60;

            // Create the forecast
            Forecast forecast = new Forecast("Test CFF", EForecastType.Snapshot, new DateTime(yr, 1, 1), duration);
            forecast.AmountBegin = 1000m;

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 1000m, new DateTime(yr, 1, 6), DateTime.Now));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 850m, new DateTime(yr, 1, 14), DateTime.Now));
            forecast.AddItem(new ForecastItem("Cell Phone", EForecastItemType.Expense, EFrequency.Monthly, 135m, new DateTime(yr, 1, 20), DateTime.Now));

            // note that the car note is on the last payment
            forecast.AddItem(new ForecastItem("Car Note", EForecastItemType.Expense, EFrequency.Monthly, 250m, new DateTime(yr, 1, 28), new DateTime(yr, 1, 28)));

            return forecast;
        }

    }

}
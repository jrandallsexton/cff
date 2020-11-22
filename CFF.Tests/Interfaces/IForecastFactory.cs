
namespace CFF.Tests.Interfaces
{

    internal interface IForecastFactory
    {
        Forecast Create();
        Forecast CreateWithExpiringItem();
    }
}
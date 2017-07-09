
using System.Linq;
using CFF.Data;
using CFF.Interfaces;

namespace CFF.Services
{
    public class CffService
    {

        private readonly CffContext _context;

        public CffService(CffContext context)
        {
            _context = context;
        }

        public Forecast Save(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);

            forecast.Items.ToList().ForEach(x =>
            {
                _context.ForecastItems.Add(x);
            });

            _context.SaveChanges();
            return forecast;
        }
    }
}
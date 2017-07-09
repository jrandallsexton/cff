
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
            _context.SaveChanges();

            //forecast.Items.ToList().ForEach(x =>
            //{
            //    x.ForecastId = forecast.Id.Value;
            //    _context.ForecastItems.Add(x);
            //});

            //_context.SaveChanges();
            return forecast;
        }

        public Forecast Get(int id)
        {
            var forecast = _context.Forecasts.FirstOrDefault(f => f.Id == id);
            if (forecast != null)
                forecast.Items = _context.ForecastItems.Where(x => x.ForecastId == id).OrderBy(x => x.Id).ToList();

            return forecast;
        }
    }
}
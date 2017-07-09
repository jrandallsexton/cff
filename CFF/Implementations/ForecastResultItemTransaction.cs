
namespace CFF.Implementations
{
    public class ForecastResultItemTransaction
    {
        public int? Id { get; set; }

        public int ForecastResultItemId { get; set; }

        public int ForecastItemId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
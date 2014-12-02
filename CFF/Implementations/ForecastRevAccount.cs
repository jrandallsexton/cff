
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Interfaces;

namespace CFF
{

    public class ForecastRevAccount : IRevolvingAccount
    {

        public decimal InitialAmount { get; set; }

        public decimal InterestRate { get; set; }

        public decimal PaymentPercent { get; set; }

        public decimal MinimumPayment { get; set; }

    }

}
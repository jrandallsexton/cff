using System;
using System.Collections.Generic;
using System.Text;
using Cff.Core.Interfaces;

namespace Cff.Core.Implementations
{
    public class ForecastRevAccount : IRevolvingAccount
    {

        public decimal InitialAmount { get; set; }

        public decimal InterestRate { get; set; }

        public decimal PaymentPercent { get; set; }

        public decimal MinimumPayment { get; set; }

    }
}

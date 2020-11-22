using System;
using System.Collections.Generic;
using System.Text;

namespace Cff.Core.Interfaces
{
    public interface IRevolvingAccount
    {
        decimal InitialAmount { get; set; }
        decimal InterestRate { get; set; }
        decimal PaymentPercent { get; set; }
        decimal MinimumPayment { get; set; }
    }
}
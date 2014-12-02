
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFF.Interfaces
{

    public interface IRevolvingAccount
    {
        decimal InitialAmount { get; set; }
        decimal InterestRate { get; set; }
        decimal PaymentPercent { get; set; }
        decimal MinimumPayment { get; set; }
    }

}
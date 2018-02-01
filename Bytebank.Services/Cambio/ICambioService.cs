using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Services.Cambio
{
    public interface ICambioService
    {
        decimal Calculate(string originCurrency, string destinyCurrency, decimal value);
    }
}

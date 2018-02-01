using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Services.Cambio
{
    public class CambioService : ICambioService
    {
        public decimal Calculate(string originCurrency, string destinyCurrency, decimal value)
        {
            var rand = new Random();
            return value * (decimal)rand.NextDouble();
        }
    }
}

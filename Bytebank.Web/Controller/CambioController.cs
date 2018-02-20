using Bytebank.Services.Cambio;
using Bytebank.Web.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Controller
{
    public class CambioController : ControllerBase
    {
        private readonly ICambioService _service;

        public CambioController()
        {
            _service = new CambioService();
        }
        public string MXN()
        {
            var finalValue = _service.Calculate("MXN", "BRL", 1);
            var text = View();
            return text.Replace("FINAL_VALUE", finalValue.ToString());
        }

        public string USD()
        {
            var finalValue = _service.Calculate("USD", "BRL", 1);
            var text = View();

            return text.Replace("FINAL_VALUE", finalValue.ToString());
        }

        public string Calculate(string originCurency, string destinyCurency, decimal value)
        {
            var finalValue = _service.Calculate(originCurency, destinyCurency, value);
            var text = View();

            return text.Replace("FINAL_VALUE", finalValue.ToString())
                .Replace("VALUE", value.ToString())
                .Replace("ORIGIN_CURENCY", originCurency)
                .Replace("DESTINY_CURENCY", destinyCurency);
        }

    }
}

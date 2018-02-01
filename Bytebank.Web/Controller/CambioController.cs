using Bytebank.Services.Cambio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Controller
{
    public class CambioController
    {
        private readonly ICambioService _service;

        public CambioController()
        {
            _service = new CambioService();
        }
        public string MXN()
        {
            var finalValue = _service.Calculate("MXN", "BRL", 1);
            var resourcePath = "Bytebank.Web.View.Cambio.MXN.html";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(resourcePath);
            var readStream = new StreamReader(resourceStream);
            var text = readStream.ReadToEnd();

            return text.Replace("FINAL_VALUE", finalValue.ToString());
        }

        public string USD()
        {
            var finalValue = _service.Calculate("USD", "BRL", 1);
            var resourcePath = "Bytebank.Web.View.Cambio.USD.html";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(resourcePath);
            var readStream = new StreamReader(resourceStream);
            var text = readStream.ReadToEnd();

            return text.Replace("FINAL_VALUE", finalValue.ToString());
        }
    }
}

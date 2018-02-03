using Bytebank.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra
{
    public class WebApplication
    {
        private readonly string[] _prefixos;

        public WebApplication(string[] prefixos)
        {
            _prefixos = prefixos ?? throw new ArgumentNullException(nameof(prefixos));
        }

        public void Iniciar()
        {
            while (true) HandleRequisition();
        }

        private void HandleRequisition()
        {
            var listener = new HttpListener();
            foreach (var prefixo in _prefixos) listener.Prefixes.Add(prefixo);
            listener.Start();

            var context = listener.GetContext();
            var response = context.Response;
            var request = context.Request;

            var path = request.Url.AbsolutePath;

            if (Utilities.IsFile(path))
            {
                var manipulator = new FileRequestManipulator();
                manipulator.Manipulate(response, path);
            }
            else if (path == "/Cambio/MXN")
            {
                var controller = new CambioController();
                var actionContent = controller.MXN();

                var resourceBytes = Encoding.UTF8.GetBytes(actionContent);
                response.OutputStream.Write(resourceBytes, 0, resourceBytes.Length);
                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.OutputStream.Close();
            }
            else if (path == "/Cambio/USD")
            {
                var controller = new CambioController();
                var actionContent = controller.USD();

                var resourceBytes = Encoding.UTF8.GetBytes(actionContent);
                response.OutputStream.Write(resourceBytes, 0, resourceBytes.Length);
                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.OutputStream.Close();
            }

            listener.Stop();
        }
    }
}

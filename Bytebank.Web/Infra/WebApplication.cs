using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var listener = new HttpListener();
            foreach (var prefixo in _prefixos) listener.Prefixes.Add(prefixo);

            listener.Start();

            var context = listener.GetContext();
            var response = context.Response;
            var request = context.Request;

            var respText = "Hello World!";
            var repoBytes = Encoding.UTF8.GetBytes(respText);
            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.ContentLength64 = repoBytes.Length;
            response.OutputStream.Write(repoBytes, 0, repoBytes.Length);
            response.OutputStream.Close();

            listener.Stop();
        }
    }
}

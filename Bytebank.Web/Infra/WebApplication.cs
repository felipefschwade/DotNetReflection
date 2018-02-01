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

            if (path == "/Assets/css/styles.css")
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resource = Utilities.PathToAssemblyConverter(path);
                var resourceStream = assembly.GetManifestResourceStream(resource);
                if (resourceStream == null)
                {
                    response.StatusCode = 404;
                    response.OutputStream.Close();
                }
                else
                {
                    var resourceBytes = new Byte[resourceStream.Length];
                    resourceStream.Read(resourceBytes, 0, (int)resourceStream.Length);
                    response.StatusCode = 200;
                    response.ContentType = "text/css";
                    response.ContentLength64 = resourceBytes.Length;
                    response.OutputStream.Write(resourceBytes, 0, resourceBytes.Length);
                    response.OutputStream.Close();
                }
            }
            else
            {
                var respText = "Hello World!";
                var repoBytes = Encoding.UTF8.GetBytes(respText);
                response.StatusCode = 200;
                response.ContentType = "text/html";
                response.ContentLength64 = repoBytes.Length;
                response.OutputStream.Write(repoBytes, 0, repoBytes.Length);
                response.OutputStream.Close();
            }
            listener.Stop();
        }
    }
}

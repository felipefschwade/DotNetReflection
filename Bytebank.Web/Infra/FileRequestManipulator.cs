using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra
{
    public class FileRequestManipulator
    {
        public void Manipulate(HttpListenerResponse response, string path)
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
                using (resourceStream)
                {
                    var resourceBytes = new Byte[resourceStream.Length];
                    resourceStream.Read(resourceBytes, 0, (int)resourceStream.Length);
                    response.StatusCode = 200;
                    response.ContentType = Utilities.GetContentTypeFor(path);
                    response.ContentLength64 = resourceBytes.Length;
                    response.OutputStream.Write(resourceBytes, 0, resourceBytes.Length);
                    response.OutputStream.Close();
                }
            }
        }
    }
}

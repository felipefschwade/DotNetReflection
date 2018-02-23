using Bytebank.Web.Infra.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra
{
    public class ControllerRequestManipulator
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();

        public void Manipulate(HttpListenerResponse response, string path)
        {
            var pieces = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            var controllerName = pieces[0];
            var controllerPath = $"Bytebank.Web.Controller.{controllerName}Controller";
            // O .NET utiliza um object handle para carregar em um appDomain diferentes os códigos "não confiáveis" da nossa aplicação.
            // Dessa forma para acessar a instância de fato, é necessário utilizar o método Unwrap
            var controllerWrapper = Activator.CreateInstance("Bytebank.Web", controllerPath, new object[0]);
            var controller = controllerWrapper.Unwrap();
            var methodInfo = _actionBinder.GetActionBindInfo(controller, path);
            var actionResult = (string)methodInfo.Invoke(controller);
            var resourceBytes = Encoding.UTF8.GetBytes(actionResult);
            response.OutputStream.Write(resourceBytes, 0, resourceBytes.Length);
            response.StatusCode = 200;
            response.ContentType = "text/html; charset=utf-8";
            response.OutputStream.Close();
        }
    }
}

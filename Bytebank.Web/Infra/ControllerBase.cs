using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName] string arquivo = null)
        {
            var diretorio = GetType().Name.Replace("Controller", "");
            var resourcePath = $"Bytebank.Web.View.{diretorio}.{arquivo}.html";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(resourcePath);
            var readStream = new StreamReader(resourceStream);
            return readStream.ReadToEnd();
        }
    }
}

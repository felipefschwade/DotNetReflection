using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra
{
    public static class Utilities
    {
        public static string PathToAssemblyConverter(string path)
        {
            return $"Bytebank.Web{path.Replace('/', '.')}";
        }

        public static string GetContentTypeFor(string path)
        {
            if (path.EndsWith(".css")) return "text/css";
            if (path.EndsWith(".js")) return "application/js";
            if (path.EndsWith(".html")) return "text/html; charset=utf-8";
            throw new NotImplementedException("Formato Inválido.");
        }

        public static bool IsFile(string path)
        {
            var splitedPath = path.Split(new char['/'], StringSplitOptions.RemoveEmptyEntries);
            var lastPart = splitedPath.Last();
            return lastPart.Contains(".");
        }

    }
}

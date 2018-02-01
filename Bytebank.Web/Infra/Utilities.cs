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
    }
}

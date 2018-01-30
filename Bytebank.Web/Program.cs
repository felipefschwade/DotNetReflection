using Bytebank.Web.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new WebApplication(new string[] { "http://localhost:54321/" });
            app.Iniciar();
        }
    }
}

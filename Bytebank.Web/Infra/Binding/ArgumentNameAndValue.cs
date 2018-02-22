using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra.Binding
{
    public class ArgumentNameAndValue
    {
        public string Nome { get; private set; }
        public string Valor { get; private set; }
        public ArgumentNameAndValue(string nome, string valor)
        {
            Nome = nome ?? throw new ArgumentNullException();
            Valor = valor ?? throw new ArgumentNullException();
        } 
    }
}

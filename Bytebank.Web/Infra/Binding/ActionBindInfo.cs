using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; set; }
        public IReadOnlyCollection<ArgumentNameAndValue> TuplesArgsAndValue { get; private set; }

        public ActionBindInfo(MethodInfo methodInfo, IEnumerable<ArgumentNameAndValue> tuplesArgsAndValue)
        {
            MethodInfo = methodInfo ?? throw new ArgumentNullException();
            TuplesArgsAndValue = new ReadOnlyCollection<ArgumentNameAndValue>(tuplesArgsAndValue.ToList());
        }

        public object Invoke(object controller)
        {
            var countTuples = TuplesArgsAndValue.Count();
            var hasArgs = countTuples > 0;
            if (!hasArgs) return MethodInfo.Invoke(controller, new object[0]);
            var parameters = MethodInfo.GetParameters();
            var invokeParams = new object[countTuples];
            for (int i = 0; i < countTuples; i++)
            {
                var parameter = parameters[i];
                var arg = TuplesArgsAndValue.Single(t => t.Nome == parameter.Name);
                invokeParams[i] = Convert.ChangeType(arg.Valor, parameter.ParameterType);
            }
            return MethodInfo.Invoke(controller, invokeParams);
        }

    }
}

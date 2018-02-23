using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra.Binding
{
    public class ActionBinder
    {
        public ActionBindInfo GetActionBindInfo(object controller, string path)
        {
            var isQueryString = path.Contains("?");

            if (!isQueryString)
            {
                var action = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var methodInfo = controller.GetType().GetMethod(action);
                return (new ActionBindInfo(methodInfo, Enumerable.Empty<ArgumentNameAndValue>()));
            }
            else
            {
                var controllerWithAction = path.Substring(0, path.IndexOf("?"));
                var action = controllerWithAction.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var queryString = path.Substring(path.IndexOf("?") + 1);
                var tuples = GetArgumentNameAndValues(queryString);
                var args = tuples.Select(p => p.Nome).ToArray();
                var methodInfo =  GetMethodInforFromNamesAndArgs(action, args, controller);
                return (new ActionBindInfo(methodInfo, tuples));
            }
        }

        private IEnumerable<ArgumentNameAndValue> GetArgumentNameAndValues(string queryString)
        {
            var tuples = queryString.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tuple in tuples)
            {
                var pieces = tuple.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                yield return new ArgumentNameAndValue(pieces[0], pieces[1]);
            }
        }

        private MethodInfo GetMethodInforFromNamesAndArgs(string actionName, string[] arguments, object controller)
        {
            var methods = controller.GetType().GetMethods();
            var overloads = methods.Where(m => m.Name == actionName);
            foreach (var overload in overloads)
            {
                var parameters = overload.GetParameters();

                if (parameters.Length != arguments.Count()) continue;
                var match = parameters.All(p => arguments.Contains(p.Name));
                if (match) return overload;
            }

            throw new ArgumentException("Sobrecarga inválida");
        }

    }
}

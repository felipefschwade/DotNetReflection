using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Web.Infra.Binding
{
    public class ActionBinder
    {
        public object GetMethodInfo(object controller, string path)
        {
            var isQueryString = path.Contains("?");

            if (!isQueryString)
            {
                var action = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                return controller.GetType().GetMethod(action);
            }
            else
            {
                var controllerWithAction = path.Substring(0, path.IndexOf("?"));
                var action = controllerWithAction.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var queryString = path.Substring(path.IndexOf("?") + 1);
                var tuples = GetArgumentNameAndValues(queryString);
            }
            return new { };
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

    }
}

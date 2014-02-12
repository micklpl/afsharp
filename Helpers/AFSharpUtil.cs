using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFSharp
{
    public class AFSharpUtil
    {
        public AggregationOptionsAttribute GetOptions(Type t)
        {
            var attrs = t.GetCustomAttributes(true);
            return attrs.OfType<AggregationOptionsAttribute>().FirstOrDefault();
        }

        public string ToCamelCase(string s)
        {
            var path = s.Split('.').Select(d => d[0].ToString().ToLower() + d.Substring(1));
            return string.Join(".", path);
        }

        public bool IsCamelCase(AggregationOptionsAttribute options)
        {
            return options != null && options.CamelCaseConvention;
        }
    }
}

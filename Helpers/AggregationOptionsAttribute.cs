using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFSharp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AggregationOptionsAttribute : Attribute
    {
        public string IdProperty { get; set; }
        public bool CamelCaseConvention { get; set; }
    }
}

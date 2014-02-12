using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AFSharp
{
    public class ComplexAggregationDescriptor<T>
    {
        private readonly PropertyNameResolver _resolver;

        public ComplexAggregationDescriptor()
        {
            _resolver = new PropertyNameResolver();
        }

        internal string LambdaToString(Expression<Func<T, object>> expr)
        {
            return string.Format("\"${0}\"", _resolver.Resolve(expr));
        }
    }
}

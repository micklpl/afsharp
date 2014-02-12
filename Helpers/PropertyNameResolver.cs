using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AFSharp
{
    public class PropertyNameResolver: AFSharpUtil
    {
        public string Resolve<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            var type = typeof (TProperty);
            string body = expression.Body.ToString();
            if(type.Name == "Object")
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if(unaryExpression != null)
                    body = unaryExpression.Operand.ToString();
            }
            var param = expression.Parameters.First() + ".";
            var str = body.Substring(param.Length);
            var options = GetOptions(typeof(TSource));
            str = ReplaceId(options, str);
            if (IsCamelCase(options))
            {
                str = ToCamelCase(str);
            }
            return str;
        }
        
        private string ReplaceId(AggregationOptionsAttribute options, string s)
        {
            if (options == null) return s;
            if (string.IsNullOrEmpty(options.IdProperty)) return s;
            return s.Replace(options.IdProperty, "_id");
        }
    }
}

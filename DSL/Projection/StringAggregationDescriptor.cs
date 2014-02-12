using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AFSharp
{
    public class StringAggregationDescriptor<T> : ComplexAggregationDescriptor<T>
    {
        public ConcatenationTerminator<T> Concat(Expression<Func<T, object>> expression)
        {
            string type = "$concat";
            return new ConcatenationTerminator<T>(type, new[] { LambdaToString(expression) });
        }

        public ConcatenationTerminator<T> Concat(string str)
        {
            string type = "$concat";
            return new ConcatenationTerminator<T>(type, new[]{StringToBsonString(str)});
        }

        public ConcatenationTerminator<T> Concat(params Expression<Func<T, object>>[] expressions)
        {
            string type = "$concat";
            return new ConcatenationTerminator<T>(type, expressions.Select(LambdaToString));
        }

        public ConcatenationTerminator<T> Concat(params string[] expressions)
        {
            string type = "$concat";
            return new ConcatenationTerminator<T>(type, expressions.Select(StringToBsonString));
        }

        public Terminator Strcasecmp(string left, string right)
        {
            string type = "$strcasecmp";
            return new Terminator(type, new[] { StringToBsonString(left), StringToBsonString(right) });
        }

        public Terminator Strcasecmp(Expression<Func<T, object>> left, string right)
        {
            string type = "$strcasecmp";
            return new Terminator(type, new[] {LambdaToString(left), StringToBsonString(right) });
        }

        public Terminator Strcasecmp(string left, Expression<Func<T, object>> right)
        {
            string type = "$strcasecmp";
            return new Terminator(type, new[] { StringToBsonString(left), LambdaToString(right) });
        }

        public Terminator Strcasecmp(Expression<Func<T, object>> left, Expression<Func<T, object>> right)
        {
            string type = "$strcasecmp";
            return new Terminator(type, new[] { LambdaToString(left), LambdaToString(right) });
        }

        public Terminator Substr(Expression<Func<T, object>> expr, int skip, int take)
        {
            string type = "$substr";
            return new Terminator(type, new[] { LambdaToString(expr), skip.ToString(), take.ToString() });
        }

        public SimpleTerminator ToLower(Expression<Func<T, object>> expr)
        {
            string type = "$toLower";
            return new SimpleTerminator(type, LambdaToString(expr));
        }

        public SimpleTerminator ToUpper(Expression<Func<T, object>> expr)
        {
            string type = "$toUpper";
            return new SimpleTerminator(type, LambdaToString(expr));
        }

        internal string StringToBsonString(string s)
        {
            return string.Format("\"{0}\"", s);
        }
    }
}

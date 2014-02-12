using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public class ArithmeticAggregationDescriptor<T> : ComplexAggregationDescriptor<T>
    {
        public AdditionTerminator Add(params Expression<Func<T, object>>[] expressions)
        {
            string type = "$add";
            return new AdditionTerminator(type, expressions.Select(LambdaToString));
        }

        public MultiplicationTerminator Multiply(params Expression<Func<T, object>>[] expressions)
        {
            string type = "$multiply";
            return new MultiplicationTerminator(type, expressions.Select(LambdaToString));
        }

        public Terminator Substract(Expression<Func<T, object>> left, 
            Expression<Func<T, object>> right)
        {
            return new Terminator("$substract", new[]{LambdaToString(left), LambdaToString(right)});
        }

        public Terminator Substract(Expression<Func<T, object>> left, object right)
        {
            return new Terminator("$substract", new[] { LambdaToString(left), right.ToString() });
        }

        public Terminator Substract(object left, Expression<Func<T, object>> right)
        {
            return new Terminator("$substract", new[] { left.ToString(), LambdaToString(right) });
        }

        public Terminator Substract(object left, object right)
        {
            return new Terminator("$substract", new[] { left.ToString(), right.ToString() });
        }

        public Terminator Divide(Expression<Func<T, object>> left,
           Expression<Func<T, object>> right)
        {
            return new Terminator("$divide", new[] { LambdaToString(left), LambdaToString(right) });
        }

        public Terminator Divide(Expression<Func<T, object>> left, object right)
        {
            return new Terminator("$divide", new[] { LambdaToString(left), right.ToString() });
        }

        public Terminator Divide(object left, Expression<Func<T, object>> right)
        {
            return new Terminator("$divide", new[] { left.ToString(), LambdaToString(right) });
        }

        public Terminator Divide(object left, object right)
        {
            return new Terminator("$divide", new[] { left.ToString(), right.ToString() });
        }

        public Terminator Mod(Expression<Func<T, object>> left,
           Expression<Func<T, object>> right)
        {
            return new Terminator("$mod", new[] { LambdaToString(left), LambdaToString(right) });
        }

        public Terminator Mod(Expression<Func<T, object>> left, object right)
        {
            return new Terminator("$mod", new[] { LambdaToString(left), right.ToString() });
        }

        public Terminator Mod(object left, Expression<Func<T, object>> right)
        {
            return new Terminator("$mod", new[] { left.ToString(), LambdaToString(right) });
        }

        public Terminator Mod(object left, object right)
        {
            return new Terminator("$mod", new[] { left.ToString(), right.ToString() });
        }
    }
}

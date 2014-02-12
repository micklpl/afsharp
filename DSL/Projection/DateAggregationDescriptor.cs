using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AFSharp
{
    public class DateAggregationDescriptor<T> : ComplexAggregationDescriptor<T>
    {
        public SimpleTerminator DayOfYear(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$dayOfYear", LambdaToString(expression));
        }

        public SimpleTerminator DayOfMonth(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$dayOfMonth", LambdaToString(expression));
        }

        public SimpleTerminator DayOfWeek(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$dayOfWeek", LambdaToString(expression));
        }

        public SimpleTerminator Year(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$year", LambdaToString(expression));
        }

        public SimpleTerminator Month(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$month", LambdaToString(expression));
        }

        public SimpleTerminator Week(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$week", LambdaToString(expression));
        }

        public SimpleTerminator Hour(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$hour", LambdaToString(expression));
        }

        public SimpleTerminator Minute(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$minute", LambdaToString(expression));
        }

        public SimpleTerminator Second(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$second", LambdaToString(expression));
        }

        public SimpleTerminator Milisecond(Expression<Func<T, object>> expression)
        {
            return new SimpleTerminator("$milisecond", LambdaToString(expression));
        }
    }
}

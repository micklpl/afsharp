using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AFSharp
{
    public class Terminator
    {
        public Terminator(string type, IEnumerable<string> values)
        {
            Type = type;
            if(values != null)
                Values = values.Select(a => (object)a).ToList();
        }

        protected List<object> Values;
        protected string Type;

        public virtual string GetResult()
        {
            var content = Values.Aggregate("", (current, value) => current + (value + ","));
            content = content.Substring(0, content.Length - 1);

            return "{ " + Type + string.Format(" : [{0}] ", content) + "}";
        }
    }

    public class AdditionTerminator: Terminator
    {
        public AdditionTerminator(string type, IEnumerable<string> values)
            : base(type, values)
        {
        }

        public AdditionTerminator Into(params string[] values)
        {
            Values.AddRange(values);
            return this;
        }

        public AdditionTerminator Into(params int[] values)
        {
            Values.AddRange(values.Select(a => (object)a));
            return this;
        }

        public AdditionTerminator Into(params float[] values)
        {
            Values.AddRange(values.Select(a => (object)a));
            return this;
        }
    }

    public class MultiplicationTerminator: Terminator
    {
        public MultiplicationTerminator(string type, IEnumerable<string> values)
            : base(type, values)
        {
        }

        public MultiplicationTerminator By(params string[] values)
        {
            Values.AddRange(values);
            return this;
        }

        public MultiplicationTerminator By(params int[] values)
        {
            Values.AddRange(values.Select(a => (object)a));
            return this;
        }

        public MultiplicationTerminator By(params float[] values)
        {
            Values.AddRange(values.Select(a => (object)a));
            return this;
        }
    }

    public class ConcatenationTerminator<T>: Terminator
    {
        public ConcatenationTerminator(string type, IEnumerable<string> values) : base(type, values)
        {
        }

        public ConcatenationTerminator<T> And(string str)
        {
            var descriptor = new StringAggregationDescriptor<T>();
            Values.Add(descriptor.StringToBsonString(str));
            return this;
        }

        public ConcatenationTerminator<T> And(params string[] str)
        {
            var descriptor = new StringAggregationDescriptor<T>();
            Values.AddRange(str.Select(descriptor.StringToBsonString));
            return this;
        }

        public ConcatenationTerminator<T> And(Expression<Func<T, object>> exp)
        {
            var descriptor = new ComplexAggregationDescriptor<T>();
            Values.Add(descriptor.LambdaToString(exp));
            return this;
        }

        public ConcatenationTerminator<T> And(params Expression<Func<T, object>>[] exps)
        {
            var descriptor = new ComplexAggregationDescriptor<T>();
            Values.AddRange(exps.Select(descriptor.LambdaToString));
            return this;
        }
    }

    public class SimpleTerminator: Terminator
    {
        private string _value;

        public SimpleTerminator(string type, string value) : base(type, null)
        {
            _value = value;
        }

        public override string GetResult()
        {
            return "{ " + Type + string.Format(" : {0} ", _value) + "}";
        }
    }
}

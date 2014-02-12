using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace AFSharp
{
    public class ProjectionDescriptor<T,T2>
    {
        private BsonDocument _document;

        public BsonDocument Result
        {
            get { return _document; }
            set { _document = value; }
        }

        public ProjectionDescriptor()
        {
            _document = new BsonDocument();
        }

        public ProjectionDescriptor<T,T2> Map()
        {
            var destProps = typeof (T2).GetProperties().Select(p => p.Name).ToList();
            var util = new AFSharpUtil();
            var isCamelCase = util.IsCamelCase(util.GetOptions(typeof (T)));
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if(destProps.Contains(propertyInfo.Name))
                {
                    var name = propertyInfo.Name;
                    if (isCamelCase)
                        name = util.ToCamelCase(name);
                    _document.Add(name, 1);
                }
            }
            return this;
        }

        public ProjectionDescriptor<T, T2> Map(params Expression<Func<T, object>>[] paths)
        {
            ExecuteMapping(paths, 1);
            return this;
        }

        public ProjectionDescriptor<T, T2> Unset(params Expression<Func<T, object>>[] paths)
        {
            ExecuteMapping(paths, 0);
            return this;
        }

        private void ExecuteMapping(Expression<Func<T, object>>[] paths, int rightSideValue)
        {
            var destProps = typeof(T2).GetProperties().Select(p => p.Name).ToList();
            var resolver = new PropertyNameResolver();
            foreach (var expression in paths)
            {
                var name = resolver.Resolve(expression);
                if (destProps.Contains(name) || name == "_id")
                {
                    _document.Add(name, rightSideValue);
                }
                else
                    throw new NotSupportedException("Property not found in destination type");
            }
        }

        public ProjectionDescriptor<T, T2> Computed(Expression<Func<T2, object>> destPath, 
            Expression<Func<T, object>> path)
        {
            var resolver = new PropertyNameResolver();
            _document.Add(resolver.Resolve(destPath), "$" + resolver.Resolve(path));
            return this;
        }

        public ProjectionDescriptor<T, T2> Arithmetic(Expression<Func<T2, object>> destPath, 
            Expression<Func<ArithmeticAggregationDescriptor<T2>, Terminator>> path)
        {
            var resolver = new PropertyNameResolver();
            var key = resolver.Resolve(destPath);
            var value = path.Compile().Invoke(new ArithmeticAggregationDescriptor<T2>()).GetResult();
            _document.Add(key, BsonDocument.Parse(value));
            return this;
        }

        public ProjectionDescriptor<T, T2> String(Expression<Func<T2, object>> destPath,
            Expression<Func<StringAggregationDescriptor<T>, Terminator>> path)
        {
            var resolver = new PropertyNameResolver();
            var key = resolver.Resolve(destPath);
            var value = path.Compile().Invoke(new StringAggregationDescriptor<T>()).GetResult();
            _document.Add(key, BsonDocument.Parse(value));
            return this;
        }

        public ProjectionDescriptor<T, T2> Date(Expression<Func<T2, object>> destPath,
            Expression<Func<DateAggregationDescriptor<T>, Terminator>> path)
        {
            var resolver = new PropertyNameResolver();
            var key = resolver.Resolve(destPath);
            var value = path.Compile().Invoke(new DateAggregationDescriptor<T>()).GetResult();
            _document.Add(key, BsonDocument.Parse(value));
            return this;
        }

        public ProjectionDescriptor<T, T2> Complex<T3>(Expression<Func<T2, T3>> destPath,
            Expression<Func<ProjectionDescriptor<T, T3>, ProjectionDescriptor<T, T3>>> path)
        {
            var resolver = new PropertyNameResolver();
            var key = resolver.Resolve(destPath);
            var value = path.Compile().Invoke(new ProjectionDescriptor<T, T3>()).Result;
            _document.Add(key, value);
            return this;
        }
    }
}

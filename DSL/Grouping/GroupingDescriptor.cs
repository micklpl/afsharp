using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public class GroupingDescriptor<T,T2>
    {
        private BsonDocument _document;

        public BsonDocument Result
        {
            get { return _document; }
            set { _document = value; }
        }

        public GroupingDescriptor(BsonDocument document)
        {
            _document = document;
        }

        public GroupingDescriptor<T, T2> Computed(Expression<Func<T2,object>> destPath, 
            Expression<Func<GroupingAggregationDescriptor<T>, BsonDocument>> aggregation)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(destPath);
            var doc = aggregation.Compile().Invoke(new GroupingAggregationDescriptor<T>());
            _document.Add(name, doc);
            return this;
        }
    }
}

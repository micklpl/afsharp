using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        public AggregationDescriptor<T2> Unwind<T2>(Expression<Func<T, object>> propertyPath)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(propertyPath);
            _pipes.Add(new BsonDocument()
                           {
                               {"$unwind", "$"+name}
                           });
            return new AggregationDescriptor<T2>(_pipes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        public AggregationDescriptor<T2> Group<T2>(Func<GroupingFactorDescriptor<T, T2>,
            GroupingDescriptor<T,T2>> descriptor)
        {
            var doc = descriptor.Invoke(new GroupingFactorDescriptor<T, T2>()).Result;
            _pipes.Add(new BsonDocument()
                           {
                               {"$group", doc}
                           });
            return new AggregationDescriptor<T2>(_pipes);
        }
    }
}

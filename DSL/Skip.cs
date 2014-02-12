using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        public AggregationDescriptor<T> Skip(int value)
        {
            _pipes.Add(new BsonDocument()
                           {
                               {"$skip", value}
                           });
            return this;
        }
    }
}

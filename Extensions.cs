using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AFSharp
{
    public static class Extensions
    {
        public static IEnumerable<T2> Aggregate<T, T2>(this MongoCollection<T> collection, 
            Func<AggregationDescriptor<T>, AggregationDescriptor<T2>> pipes)
        {
            var pipeline = pipes.Invoke(new AggregationDescriptor<T>());
            AggregateResult result = collection.Aggregate(pipeline.GetPipeline());
            return from doc in result.ResultDocuments select BsonSerializer.Deserialize<T2>(doc);
        }
    }
}

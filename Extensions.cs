using System;
using MongoDB.Driver;

namespace AFSharp
{
    public static class Extensions
    {
        public static AggregateResult Aggregate<T, T2>(this MongoCollection<T> collection, 
            Func<AggregationDescriptor<T>, AggregationDescriptor<T2>> pipes)
        {
            var pipeline = pipes.Invoke(new AggregationDescriptor<T>());
            AggregateResult result = collection.Aggregate(pipeline.GetPipeline());
            return result;
        }
    }
}

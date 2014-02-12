using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        public AggregationDescriptor<T2> Project<T2>(Func<ProjectionDescriptor<T, T2>,
            ProjectionDescriptor<T,T2>> projections)
        {
            var pipes = GetPipeline().ToList();
            var result = projections.Invoke(new ProjectionDescriptor<T, T2>()).Result;
            pipes.Add(new BsonDocument()
                          {
                              {"$project", result}
                          });
            return new AggregationDescriptor<T2>(pipes){};
        }
    }
}

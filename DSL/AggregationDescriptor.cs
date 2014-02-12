using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        private readonly List<BsonDocument> _pipes;

        public BsonDocument[] GetPipeline()
        {
            return _pipes.ToArray();
        }

        public AggregationDescriptor()
        {
            _pipes = new List<BsonDocument>();
        }

        public AggregationDescriptor(List<BsonDocument> pipes)
        {
            _pipes = pipes;
        }
    }
}

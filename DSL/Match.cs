using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        public AggregationDescriptor<T> Match(IMongoQuery q)
        {
            BsonDocument doc = new BsonDocument();
            var util = new AFSharpUtil();
            bool isCamelCase = false;
            var options = util.GetOptions(typeof (T));
            if (options != null)
            {
                isCamelCase = options.CamelCaseConvention;
            }

            foreach (var pair in q.ToBsonDocument())
            {
                var name = pair.Name;
                if(isCamelCase)
                    name = util.ToCamelCase(name);
                doc.Add(name, pair.Value);
            }

            _pipes.Add(new BsonDocument()
                           {
                               {"$match", doc}
                           });
            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AFSharp
{
    public class MongoQueryAnalyzer: AFSharpUtil
    {
        public BsonDocument Analyze<T>(IMongoQuery query)
        {
            var q = query.ToJson();
            var options = GetOptions(typeof (T));
            if(options == null || !options.CamelCaseConvention)
                return BsonDocument.Parse(q);
            var doc = new BsonDocument();
            foreach (var keyValue in BsonDocument.Parse(q))
            {
                doc.Add(ToCamelCase(keyValue.Name), keyValue.Value);
            }
            return doc;
        }
    }
}

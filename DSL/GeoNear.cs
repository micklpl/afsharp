using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace AFSharp
{
    public partial class AggregationDescriptor<T>
    {
        public AggregationDescriptor<T2> GeoNear<T2>(GeoJson2DCoordinates coordinates, 
            Expression<Func<T2,object>> distanceField, int limit = 100, float maxDistance = float.NaN,
            IMongoQuery query = null, bool spherical = false, int distanceMultiplier = 1,
            Expression<Func<T2, object>> includeLocs = null, bool uniqueDocs = false)
        {
            var resolver = new PropertyNameResolver();
            var analyzer = new MongoQueryAnalyzer();
            var document = new BsonDocument
                               {
                                   {"near", new BsonArray(new []{coordinates.X, coordinates.Y})},
                                   {"distanceField", resolver.Resolve(distanceField)}
                               };

            if(!float.IsNaN(maxDistance))
                document.Add("maxDistance", maxDistance);
            if (query != null)
                document.Add("query", analyzer.Analyze<T>(query));
            if (includeLocs != null)
                document.Add("includeLocs", resolver.Resolve(includeLocs));
            if (uniqueDocs)
                document.Add("uniqueDocs", true);
            if (limit != 100)
                document.Add("limit", limit);

            _pipes.Add(new BsonDocument()
                           {
                               {"$geoNear", document}
                           });
            return new AggregationDescriptor<T2>(_pipes);
        }
    }
}

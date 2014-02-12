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
        public AggregationDescriptor<T> Sort<T2>(Expression<Func<T, T2>> propertyPath, 
            SortDescription order = SortDescription.Ascending)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(propertyPath);

            var @params = new BsonDocument()
                          {
                              {name, (int)order}
                          };

            var doc = new BsonDocument()
                          {
                              { "$sort", @params }
                          };

            MergeSortBsons(doc);
            return this;
        }

        private void MergeSortBsons(BsonDocument bson)
        {
            var last = _pipes.LastOrDefault();
            if(last != null && last.Contains("$sort"))
            {
                var by = last["$sort"].AsBsonDocument;
                by.AddRange(bson["$sort"].AsBsonDocument);
            }
            else
            {
                _pipes.Add(bson);
            }
        }
    }

    public enum SortDescription
    {
        Ascending = 1,
        Descending = -1
    }
}

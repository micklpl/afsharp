using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public class GroupingFactorDescriptor<T,T2>
    {
        public GroupingDescriptor<T,T2> By(Expression<Func<T, object>> path)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(path);
            var doc = new BsonDocument()
                          {
                              {"_id", "$" + name}
                          };
            return new GroupingDescriptor<T, T2>(doc);
        }

        public GroupingDescriptor<T, T2> By<T3>(Func<ProjectionDescriptor<T, T3>,
            ProjectionDescriptor<T, T3>> projection)
        {
            var doc = projection.Invoke(new ProjectionDescriptor<T, T3>()).Result;
            var document = new BsonDocument();
            foreach (var key in doc)
            {
                if (key.Value == 1 || key.Value == 0)
                    document.Add(key.Name, string.Format("${0}", key.Name));
                else
                    document.Add(key);
            }
            return new GroupingDescriptor<T, T2>(new BsonDocument()
                                                     {
                                                         {"_id", document}
                                                     });
        }
    }
}

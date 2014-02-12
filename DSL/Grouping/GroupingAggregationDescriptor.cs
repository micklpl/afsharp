using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;

namespace AFSharp
{
    public class GroupingAggregationDescriptor<T>
    {
        public BsonDocument Count()
        {
            return new BsonDocument()
                       {
                           {"$sum", 1}
                       };
        }

        public BsonDocument Sum(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$sum", "$" + name}
                          };
            return doc;
        }

        public BsonDocument Min(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$min", "$" + name}
                          };
            return doc;
        }

        public BsonDocument Max(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$max", "$" + name}
                          };
            return doc;
        }

        public BsonDocument Avg(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$avg", "$" + name}
                          };
            return doc;
        }

        public BsonDocument First(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$first", "$" + name}
                          };
            return doc;
        }

        public BsonDocument Last(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$last", "$" + name}
                          };
            return doc;
        }

        public BsonDocument AddToSet(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$addToSet", "$" + name}
                          };
            return doc;
        }

        public BsonDocument Push(Expression<Func<T, object>> expression)
        {
            var resolver = new PropertyNameResolver();
            var name = resolver.Resolve(expression);
            var doc = new BsonDocument()
                          {
                              {"$push", "$" + name}
                          };
            return doc;
        }
    }
}

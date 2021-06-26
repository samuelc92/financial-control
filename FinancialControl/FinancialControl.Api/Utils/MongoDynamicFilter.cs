using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace FinancialControl.Api.Infra.Database
{
    public static class MongoDynamicFilter
    {
        //TODO: Refactor method 
        public static FilterDefinition<TMapper> Filter<TMapper>(Type t, Dictionary<string, string> param)
        {
            if (param is null || param.Count == 0) return FilterDefinition<TMapper>.Empty;
            var builder = Builders<TMapper>.Filter;
            FilterDefinition<TMapper> filter = null;
            var fieldInfo = t.GetProperties();
            var dateTimeFilterCount = 0;
            foreach (var keyValuePair in param)
            {
                var field = fieldInfo.FirstOrDefault(p => p.Name.Equals(keyValuePair.Key));
                if (field == null) continue;
                if (field.PropertyType == typeof(DateTime))
                {
                    if (dateTimeFilterCount == 0)
                    {
                        filter = filter == null
                            ? builder.Gte(keyValuePair.Key, keyValuePair.Value)
                            : filter & builder.Gte(keyValuePair.Key, keyValuePair.Value);
                    }
                    else 
                    {
                        filter = filter == null
                            ? builder.Lte(keyValuePair.Key, keyValuePair.Value)
                            : filter & builder.Lte(keyValuePair.Key, keyValuePair.Value);
                    }

                    dateTimeFilterCount++;
                }
                else
                {
                    filter = filter == null
                        ? builder.Eq(keyValuePair.Key, keyValuePair.Value)
                        : filter & builder.Eq(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return filter;
        }
    }
}
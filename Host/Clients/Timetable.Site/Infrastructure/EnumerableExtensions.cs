using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static EnumerableWithTotal<TModel> ToEnumerableWithTotal<TSource, TModel>(
            this IEnumerable<TSource> source,
            Func<TSource, TModel> projection)
        {
            if (projection == null) 
                throw new ArgumentNullException();

            var enumerable = source as TSource[] ?? source.ToArray();

            return new EnumerableWithTotal<TModel>
            {
                Items = enumerable.Select(projection).ToList(),
                Total = enumerable.Count()
            };
        }
    }
}
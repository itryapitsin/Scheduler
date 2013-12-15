using System;
using System.Collections.Generic;
using System.Linq;

namespace Timetable.Site.Infrastructure
{
    public static class EnumerableWithTotalExtensions
    {
        /// <summary>
        /// Transform an enumerable with total into a collection response
        /// </summary>
        /// <typeparam name="TSource">Type of source collection items</typeparam>
        /// <typeparam name="TModel">Type of records the response model is holding</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="projection">A projection applied to every element of the source</param>
        public static CollectionResponse<TModel> ToCollectionResponse<TSource, TModel>(
            this EnumerableWithTotal<TSource> source,
            Func<TSource, TModel> projection)
        {
            if (projection == null) throw new ArgumentNullException();
            return new CollectionResponse<TModel>
            {
                Items = source.Items.Select(projection),
                Total = source.Total
            };
        }

        /// <summary>
        /// Transform an enumerable with total into a collection response of the same item type
        /// </summary>
        /// <typeparam name="TModel">Type of collection items</typeparam>
        /// <param name="source">Source collection</param>
        public static CollectionResponse<TModel> ToCollectionResponse<TModel>(this EnumerableWithTotal<TModel> source)
        {
            return ToCollectionResponse(source, sourceItem => sourceItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static CollectionResponse<TModel> ToCollectionResponse<TSource, TModel>(
            this IQueryable<TSource> source, 
            Func<TSource, TModel> projection)
        {
            return ToCollectionResponse(source, projection, new CollectionRequest());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static CollectionResponse<TModel> ToCollectionResponse<TSource, TModel>(
            this ICollection<TSource> source, 
            Func<TSource, TModel> projection)
        {
            return ToCollectionResponse(source, projection, new CollectionRequest());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <param name="request"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static CollectionResponse<TModel> ToCollectionResponse<TSource, TModel>(
            this IQueryable<TSource> source,
            Func<TSource, TModel> projection,
            ICollectionRequest request)
        {
            var total = source.Count();
            var materializedItems = source
                .Skip((int)request.Offset)
                .Take((int)request.Limit)
                .ToList();

            var result = new EnumerableWithTotal<TSource>
            {
                Total = total, 
                Items = materializedItems
            };

            return result.ToCollectionResponse(projection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        /// <param name="request"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static CollectionResponse<TModel> ToCollectionResponse<TSource, TModel>(
            this ICollection<TSource> source,
            Func<TSource, TModel> projection,
            ICollectionRequest request)
        {
            var total = source.Count();
            var materializedItems = source
                .Skip((int)request.Offset)
                .Take((int)request.Limit)
                .ToList();

            var result = new EnumerableWithTotal<TSource>
            {
                Total = total,
                Items = materializedItems
            };

            return result.ToCollectionResponse(projection);
        }
    }
}
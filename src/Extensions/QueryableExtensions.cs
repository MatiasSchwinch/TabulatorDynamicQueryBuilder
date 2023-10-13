using Microsoft.EntityFrameworkCore;
using TabulatorDynamicQueryBuilder.Models;
using TabulatorDynamicQueryBuilder.Utilities;

namespace TabulatorDynamicQueryBuilder.Extensions
{
    public static class QueryableExtensions
    {
        public static TabulatorDynamicQueryBuilder<T> TabulatorQueryBuilder<T>(this IQueryable<T> source, TabulatorOptionsModel tabulatorOptions, bool asNoTracking = true) where T : class
        {
            return new TabulatorDynamicQueryBuilder<T>(asNoTracking ? source.AsNoTracking() : source, tabulatorOptions);
        }
    }
}

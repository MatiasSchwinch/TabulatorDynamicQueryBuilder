using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;
using TabulatorDynamicQueryBuilder.Models;

namespace TabulatorDynamicQueryBuilder.Utilities
{
    /// <summary>
    ///     <para>
    ///         Class to transform Tabulator library requests into EF Core queries.
    ///     </para>
    ///     <para>
    ///         Additional documentation <see href="https://tabulator.info/docs/5.5/data#ajax"/>
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TabulatorDynamicQueryBuilder<T>
    {
        public TabulatorOptionsModel Options => _options;

        private IQueryable<T> _source;
        private TabulatorOptionsModel _options;

        public TabulatorDynamicQueryBuilder(IQueryable<T> source, TabulatorOptionsModel options)
        {
            _source = source;
            _options = options;
        }

        public async Task<TabulatorResponseModel<T>> Build()
        {
            if (_options.Filter != null && _options.Filter.Any())
            {
                _source = ApplyFilters(_source, _options.Filter);
            }

            if (_options.Sort != null && _options.Sort.Any())
            {
                _source = ApplySorting(_source, _options.Sort);
            }

            var total = _source.Count();
            var pages = (int)Math.Ceiling((double)total / _options.Size);

            _source = ApplyPagination(_source, _options.Page, _options.Size);

            return new TabulatorResponseModel<T>
            {
                Last_page = pages,
                Data = await _source.ToListAsync()
            };
        }

        /// <summary>
        ///     Adds to the EF Core query the criteria by which it should be filtered.
        /// </summary>
        public IQueryable<T> ApplyFilters(IQueryable<T> source, IEnumerable<TabulatorFilterModel> filters)
        {
            foreach (var filter in filters)
            {
                var filterExpression = GetFilterExpression(filter);
                source = source.Where(filterExpression);
            }

            return source;
        }

        /// <summary>
        ///     <para>
        ///         Constructs an expression with the values received from <see cref="TabulatorFilterModel"/>.
        ///     </para>
        ///     <para>
        ///         Additional documentation on tabulator filters: <see href="https://tabulator.info/docs/5.5/filter"/>
        ///     </para>
        /// </summary>
        /// <param name="filter">Filters received from tabulator, for more information see the following documentation.</param>
        /// <returns>Lambda expression already built to be used in the method <see cref="ApplyFilters(IQueryable{T}, IEnumerable{TabulatorFilterModel})"/></returns>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="NotSupportedException">asd</exception>
        private Expression<Func<T, bool>> GetFilterExpression(TabulatorFilterModel filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filter.Field);

            // Convert from string to property type.
            var converter = TypeDescriptor.GetConverter(property.Type);
            if (!converter.CanConvertFrom(typeof(string)))
            {
                throw new InvalidCastException($"Cannot convert value {filter.Value} to {property.Type}");
            }
            
            var convertedValue = converter.ConvertFrom(filter.Value);
            var constant = Expression.Constant(convertedValue, property.Type);

            Expression body = filter.Type switch
            {
                //"like" => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) })!, constant, Expression.Constant(StringComparison.InvariantCultureIgnoreCase)),
                "like" => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant),
                "=" => Expression.Equal(property, constant),
                "!=" => Expression.NotEqual(property, constant),
                ">" => Expression.GreaterThan(property, constant),
                ">=" => Expression.GreaterThanOrEqual(property, constant),
                "<" => Expression.LessThan(property, constant),
                "<=" => Expression.LessThanOrEqual(property, constant),
                "starts" => Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, constant),
                "ends" => Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!, constant),
                _ => throw new NotSupportedException($"Filter type is not compatible: {filter.Type}"),
            };
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        ///     Adds to the EF Core query the criteria by which it should be sorted.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public IQueryable<T> ApplySorting(IQueryable<T> source, IEnumerable<TabulatorSortModel> sorts)
        {
            IOrderedQueryable<T>? orderedQuery = null;

            foreach (var sort in sorts)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, sort.Field);
                var orderByExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

                orderedQuery = sort.Dir switch
                {
                    "asc" => orderedQuery == null
                        ? source.OrderBy(orderByExpression)
                        : orderedQuery.ThenBy(orderByExpression),
                    "desc" => orderedQuery == null
                        ? source.OrderByDescending(orderByExpression)
                        : orderedQuery.ThenByDescending(orderByExpression),
                    _ => throw new NotSupportedException($"The sort order is not compatible: {sort.Dir}"),
                };
            }

            return orderedQuery ?? source;
        }

        /// <summary>
        ///     Adds to the EF Core query the criteria with which to paginate the information.
        /// </summary>
        /// <param name="page">Current page in the table</param>
        /// <param name="size">Number of records to take</param>
        public IQueryable<T> ApplyPagination(IQueryable<T> source, int page, int size)
        {
            return source
                .Skip((page - 1) * size)
                .Take(size);
        }
    }
}

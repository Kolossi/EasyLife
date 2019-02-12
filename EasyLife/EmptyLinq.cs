using System;
using System.Collections.Generic;
using System.Linq;

namespace Kolossi.EasyLife
{
    /// <summary>
    /// Empty Linq (aka EasyLife Linq) by Kolossi :
    /// Accepts null inputs as if empty, and will always return empty not null
    /// no more 'value cannot be null, parameter name: source'!
    /// </summary>

    public static class EmptyLinq
    {
        #region Linq replacements

        /// <summary>
        /// Easygoing Select - return empty if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ESelectN<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) return Enumerable.Empty<TResult>();

            return source.Select(selector);
        }

        /// <summary>
        /// Easygoing Select - return empty if source null and strip any null values before and after evaluating selector
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ESelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) return Enumerable.Empty<TResult>();

            return source.Where(i => i != null).Select(selector);
        }

        /// <summary>
        /// Easygoing SelectMany - return empty if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ESelectManyN<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null) return Enumerable.Empty<TResult>();

            return source.Where(i => i != null).Select(selector).Where(i => i != null).SelectMany(i => i);
        }

        /// <summary>
        /// Easygoing SelectMany - return empty if source null and strip null values before and after evaluating selector
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ESelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null) return Enumerable.Empty<TResult>();

            return source.Where(i => i != null).Select(selector).Where(i => i != null).SelectMany(i => i).Where(i => i != null);
        }

        /// <summary>
        /// Easygoing Union - return appropriate value or empty if first and/or second are null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> EUnionN<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            IEnumerable<TSource> result;
            if (first != null)
            {
                if (second != null)
                {
                    result = first.Union(second);
                }
                else
                {
                    result = first;
                }
            }
            else
            {
                if (second != null)
                {
                    result = second;
                }
                else
                {
                    return Enumerable.Empty<TSource>();
                }
            }
            return result;
        }
        /// <summary>
        /// Easygoing Union - return appropriate value or empty if first and/or second are null and strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> EUnion<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            IEnumerable<TSource> result;
            if (first != null)
            {
                if (second != null)
                {
                    result = first.Union(second);
                }
                else
                {
                    result = first;
                }
            }
            else
            {
                if (second != null)
                {
                    result = second;
                }
                else
                {
                    return Enumerable.Empty<TSource>();
                }
            }
            return result.Where(i => i != null);
        }

        /// <summary>
        /// Easygoing Where - return empty if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> EWhereN<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) return Enumerable.Empty<TSource>();

            return source.Where(predicate);
        }

        /// <summary>
        /// Easygoing Where - return empty if source null and strip null values before evaluating predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> EWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) return Enumerable.Empty<TSource>();

            return source.Where(i => i != null).Where(predicate);
        }


        /// <summary>
        /// Easygoing Any - return false if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool EAnyN<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) return false;

            return source.Any();
        }

        /// <summary>
        /// Easygoing Any - return false if source null and strip null values before evaluating
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool EAny<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) return false;

            return source.Any(i => i != null);
        }

        /// <summary>
        /// Easygoing Any - return false if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool EAnyN<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) return false;

            return source.Any(predicate);
        }
        
        /// <summary>
        /// Easygoing Any - return false if source null and strip null values before evaluating predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool EAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) return false;

            return source.Where(i => i != null).Any(predicate);
        }

        /// <summary>
        /// Easygoing Any - return 0m if source null
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ESum(this IEnumerable<decimal> source)
        {
            if (source == null) return 0m;

            return source.Sum();
        }

        /// <summary>
        /// Easygoing Any - return 0m if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static decimal ESumN<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            if (source == null) return 0m;

            return source.Sum(selector);
        }

        /// <summary>
        /// Easygoing Sum - return 0m if source null and strip null values before evaluating
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static decimal ESum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            if (source == null) return 0m;

            return source.Where(i => i != null).Sum(selector);
        }

        /// <summary>
        /// Easygoing First - return first non-null value, return default value if source null or empty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource EFirst<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any()) return default(TSource);

            return source.Where(i => i != null).FirstOrDefault();
        }

        /// <summary>
        /// Easygoing First - return default value if source null or empty, dont strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource EFirstN<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any()) return default(TSource);

            return source.FirstOrDefault();
        }

        /// <summary>
        /// Easygoing First - return default value if source null or empty and strip null values before evaluating predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource EFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null || !source.Any()) return default(TSource);

            return source.Where(i => i != null).FirstOrDefault(predicate);
        }

        /// <summary>
        /// Easygoing First - return default value if source null or empty, dont strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource EFirstN<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null || !source.Any()) return default(TSource);

            return source.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Easygoing Contains - return false if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EContainsN<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            if (source == null) return false;
            return source.Contains(value);
        }

        /// <summary>
        /// Easygoing Contains - return false if source null and strip null values before evaluating
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EContains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            if (source == null) return false;

            return source.Where(i => i != null).Contains(value);
        }

        /// <summary>
        /// Easygoing Contains - return false if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool EContainsN<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (source == null) return false;
            return source.Contains(value, comparer);
        }

        /// <summary>
        /// Easygoing Contains - return false if source null and strip null values before evaluating
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool EContains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (source == null) return false;
            return source.Where(i => i != null).Contains(value, comparer);
        }

        /// <summary>
        /// Easygoing Count - return 0 if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ECountN<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) return 0;

            return source.Count();
        }

        /// <summary>
        /// Easygoing Count - return 0 if source null and strip null values before evaluating
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ECount<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) return 0;

            return source.Count(i => i != null);
        }

        /// <summary>
        /// Easygoing Count - return 0 if source null, don't strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int ECountN<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) return 0;

            return source.Count(predicate);
        }

        /// <summary>
        /// Easygoing Count - return 0 if source null and strip null values before evaluating
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int ECount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) return 0;

            return source.Where(i => i != null).Count(predicate);
        }

        #endregion



        #region Other Non-Linq utils

        /// <summary>
        /// Easygoing min datetime - return DateTime.MinValue if source is null or empty, exclude any MinValue or MaxValue before finding min
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime EMinDateTimeValue(this IEnumerable<DateTime> source)
        {
            if (!source.EAny()) return DateTime.MinValue;

            var values = source.EWhere(d => d != DateTime.MinValue && d != DateTime.MaxValue);

            if (!values.Any()) return DateTime.MinValue;

            return values.Min();
        }

        /// <summary>
        /// Easygoing max datetime - return DateTime.MinValue if source is null or empty, exclude any MinValue or MaxValue before finding max
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime EMaxDateTimeValue(this IEnumerable<DateTime> source)
        {
            if (!source.EAny()) return DateTime.MinValue;

            var values = source.EWhere(d => d != DateTime.MinValue && d != DateTime.MaxValue);

            if (!values.Any()) return DateTime.MinValue;

            return values.Max();
        }

        /// <summary>
        /// Easygoing Except - standard linq Except removes duplicates this won't. Will not strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ENonDistinctExceptN<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null) return Enumerable.Empty<TSource>();
            if (second == null) return first;

            return first.Where(x => !second.Any(y => EqualityComparer<TSource>.Default.Equals(x, y)));
        }

        /// <summary>
        /// Easygoing Except - standard linq Except removes duplicates this won't, but it will strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ENonDistinctExcept<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null) return Enumerable.Empty<TSource>();
            if (second == null) return first;

            return first.EWhere(x => !second.EAny(y => EqualityComparer<TSource>.Default.Equals(x, y)));
        }

        /// <summary>
        /// Easygoing Except - standard linq Except removes duplicates this won't. Will not strip null values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ENonDistinctExceptN<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null) return Enumerable.Empty<TSource>();
            if (second == null) return first;

            return first.Where(x => !second.Any(y => comparer.Equals(x, y)));
        }

        /// <summary>
        /// Easygoing Except - standard linq Except removes duplicates this won't, but it will strip null values before evaluating comparer
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ENonDistinctExcept<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null) return Enumerable.Empty<TSource>();
            if (second == null) return first;

            return first.EWhere(x => !second.EAny(y => comparer.Equals(x, y)));
        }

        #endregion
    }
}

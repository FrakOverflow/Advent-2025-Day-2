namespace Advent_2025_Day_2
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    static internal class SkuValidatorP1
    {
        /// <summary>
        /// Get all invalid SKUs from a list of ranges in parallel.
        /// </summary>
        /// <param name="ranges">List of ranges and Tuple<long, long></param>
        /// <returns>A list of invalid SKUs as Longs</returns>
        internal static List<long> GetInvlidSkus(List<Tuple<long, long>> ranges)
        {
            // Get invalid SKUs in parallel
            var results = new ConcurrentBag<long>();

            Parallel.ForEach(ranges, range =>
            {
                foreach (var sku in LRange(range.Item1, range.Item2))
                {
                    if (!IsValidSku(sku))
                    {
                        results.Add(sku);
                    }
                }
            });

            // Return results as a list
            return [.. results];

        }

        /// <summary>
        /// Get all even length ranges from a comma-separated string of ranges in parallel.
        /// </summary>
        /// <param name="idRanges">List of ranges with ranges seperated by '-' and list items delimted with ','</param>
        /// <returns>A list of tuples representing the even length ranges in the given list of ranges</returns>
        internal static List<Tuple<long, long>> GetEvenRangesParallel(string idRanges)
        {
            // Create list of tuples from input ranges
            var splitRanges = idRanges.Split(',')
                .Select(range =>
                {
                    var splitNumbers = range.Split('-');
                    return Tuple.Create(splitNumbers[0], splitNumbers[1]);
                })
                .ToList();

            // Use concurrent bag to get ranges in parallel
            var result = new ConcurrentBag<Tuple<long, long>>();

            Parallel.ForEach(splitRanges, range =>
            {
                var evenLengthRanges = GetEvenLengthRanges(range.Item1, range.Item2);
                foreach (var evenRange in evenLengthRanges)
                {
                    result.Add(evenRange);
                }
            });

            // Return as a list
            return [.. result];
        }

        /// <summary>
        /// Given a range of numbers, return a list of ranges where the numbers have even lengths.
        /// </summary>
        /// <param name="start">Range Start (inclusive)</param>
        /// <param name="end">Range End (inclusive)</param>
        /// <returns>List of Tuples representing all even length ranges in the given range</returns>
        private static List<Tuple<long, long>> GetEvenLengthRanges(string start, string end)
        {
            var result = new List<Tuple<long, long>>();

            // If start and end have the same length (Base Case)
            if (start.Length == end.Length)
            {
                // Return the whole range if the length is even, otherwise return an empty list
                if (start.Length % 2 == 0)
                {
                    result.Add(Tuple.Create(long.Parse(start), long.Parse(end)));
                }
            }
            // If start and end have different lengths (Recursive Case)
            else
            {
                // If start length is even, add range from start to the largest number with the same length
                if (start.Length % 2 == 0)
                {
                    result.Add(Tuple.Create(long.Parse(start), (long)Math.Pow(10, start.Length) - 1));
                }

                var nextStart = ((long)Math.Pow(10, start.Length)).ToString();
                var recursion = GetEvenLengthRanges(nextStart, end);

                result.AddRange(recursion);
            }

            return result;
        }

        /// <summary>
        /// Generate a sequence of long integers with deferred execution.
        /// </summary>
        /// <param name="start">Range Start</param>
        /// <param name="end">Range End</param>
        /// <returns>Range of values from Start to End</returns>
        private static IEnumerable<long> LRange(long start, long end)
        {
            for (long i = start; i <= end; i++)
            {
                yield return i;
            }
        }

        /// <summary>
        /// Check whether a given SKU is valid
        /// </summary>
        /// <param name="sku">SKU as a Long</param>
        /// <returns>Bool indicating sku validity</returns>
        private static bool IsValidSku(long sku)
        {
            return IsValidSku(sku.ToString());
        }

        /// <summary>
        /// Check whether a given SKU is valid
        /// </summary>
        /// <param name="sku">SKU as a string</param>
        /// <returns>Bool indicating sku validity</returns>
        private static bool IsValidSku(string sku)
        {
            //Console.WriteLine($"Checking SKU: {sku}\nhalf 1: {sku[..(sku.Length / 2)]}\nhalf 2: {sku[(sku.Length / 2)..]}");
            return sku[..(sku.Length / 2)] != sku[(sku.Length / 2)..];
        }
    }
}

namespace Advent_2025_Day_2
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class SkuValidatorP2
    {
        /// <summary>
        /// Get all invalid SKUs from a list of ranges in parallel.
        /// </summary>
        /// <param name="ranges">List of ranges and Tuple<long, long></param>
        /// <returns>A list of invalid SKUs as Longs</returns>
        internal static long GetInvalidSkuSum(List<Tuple<long, long>> ranges)
        {
            // Get invalid SKUs in parallel (modified to return sum instead of list)
            // var results = new ConcurrentBag<long>();
            var sum = 0L;

            Parallel.ForEach(ranges, range =>
            {
                foreach (var sku in LRange(range.Item1, range.Item2))
                {
                    if (!IsValidSku(sku))
                    {
                        // results.Add(sku);
                        Interlocked.Add(ref sum, sku); // Thread-safe addition to sum
                    }
                }
            });

            // Return Sum of invalid SKUs
            return sum;

        }

        /// <summary>
        /// Get all ranges from a comma-separated string of ranges.
        /// </summary>
        /// <param name="idRanges">List of ranges with ranges seperated by commas and range start/end valuse seperated by hyphen</param>
        /// <returns>A list of all SKU ranges as tuples</returns>
        internal static List<Tuple<long, long>> GetRanges(string idRanges)
        {
            return idRanges.Split(',')
                .Select(range =>
                {
                    var splitNumbers = range.Split('-');
                    return Tuple.Create(long.Parse(splitNumbers[0]), long.Parse(splitNumbers[1]));
                })
                .ToList();
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
            var isValid = true;

            // Get all possible factors of sku length and use them to partition the sku for validation
            var factors = GetFactors(sku.Length);

            foreach (var factor in factors)
            {
                isValid = ValidatePartitionedSku(sku, factor);

                if(!isValid)
                {
                    Console.WriteLine($"- {sku} is Invalid at partition length {factor}");
                    break;
                }
            }
            
            return isValid;
        }

        /// <summary>
        /// Check if sku is valid when partitioned into chunks of given length.
        /// </summary>
        /// <param name="sku">SKU</param>
        /// <param name="partition">Partition size </param>
        /// <returns>Boolean indicating SKU validity for the given partition length</returns>
        private static bool ValidatePartitionedSku(string sku, int partition)
        {
            if (partition == sku.Length)
            {
                // A partition equal to the length of the SKU is always valid
                return true;
            }

            var chunks = sku.Chunk(partition).Select(chunk => new string(chunk)).ToList();

            // Compare each chunk to the first chunk
            foreach (var chunk in chunks)
            {
                // If any chunk is different, the SKU is valid at this partition length
                if (chunk != chunks[0])
                {
                    return true;
                }
            }

            // Otherwise, if all chunks are the same, the SKU is invalid
            return false;
        }

        /// <summary>
        /// Get factors for a given number n (deferred execution).
        /// </summary>
        /// <param name="n">Number to get factors for</param>
        /// <returns>List of factors as ints</returns>
        private static IEnumerable<int> GetFactors(long n)
        {
            // Loops from 1 to the square root of the number to find factors
            for (int i = 1; i <= Math.Sqrt(n); i++)
            {
                // i is a factor of n iff n mod i is 0
                if (n % i == 0)
                {
                    yield return i;
                    if (i != n / i)
                    {
                        yield return (int)(n / i);
                    }
                }
            }
        }
    }
}

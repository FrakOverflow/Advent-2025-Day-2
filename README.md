# Advent of Code 2025 - Day 2 - Parts 1 and 2 (COMPLETED)

This repository contains my solution for Day 2 of the Advent of Code 2025 challenge, implemented in C#. The problem description is available on the [Advent of Code website](https://adventofcode.com/2025/day/2).

## Solution Overview

For part 1 of this solution I started with a static library class again. I initially broke the problem down into 2 parts:
1. Removing invalid IDs based on heuristics to avoid processing every number in the range
2. Processing the remaining numbers by comparing the 2 halves

The first issue I ran into was in processing such large numbers, I had to switch all the arithmetic to Longs to avoid going out of range. Other than that the first half was quick to complete. However, my approach quickly fell apart in part 2 because the invalid IDs are a lot more general, it was difficult to find good heuristics to prune the set. I removed the heuristics and just compared all the numbers in even size chunks using their factors to divide chunks evenly. 

I initially completed part 2 with a static library of methods, but realized I could benefit from an object oriented approach as it allows me to memoize the factorization function. We are factoring the length so the processing shouldn't get too crazy here, but given large enough numbers it could become an issue. Additionally, we are seeing many repeated lengths which is the optimal use case for memoization.

## Review
The Good: I did a great job parallelizing this solution. I ran into some small snags by accidentally using nonconcurrent classes but I worked it out and I really love how I wrote this!

The Bad: There may be more complex heuristics for filtering the valid SKUs that I did not consider. There are other ways to parallelize this that may be more efficient, I just did some basic complexity analysis as I was writing but I bet some improvements could be found by benchmarking and playing around with the parallelization.

The Ugly: Again, this could have a nice UI to go with but who doesn't love a good console :p The LINQ statement in the GetRanges function is also super gross, not a big fan of that but its compact and functional (this could be parallelized too, could help with very large lists).

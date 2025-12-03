using Advent_2025_Day_2;

Console.WriteLine("Advent of Code 2025");
Console.WriteLine("--- Day 2: Gift Shop ---");

var testPath = "./testInput.txt";
var inputPath = "./realInput.txt";

try
{
    /* Part 1 Solution ---------------------------------------------------
    var idRanges = File.ReadAllLines(inputPath).First();

    var evenRanges = SkuValidatorP1.GetEvenRangesParallel(idRanges);

    Console.WriteLine($"Found {evenRanges.Count} even length ranges:");
    foreach (var range in evenRanges)
    {
        Console.WriteLine($" - {range.Item1}-{range.Item2}");
    }

    var invalidSkus = SkuValidatorP1.GetInvlidSkus(evenRanges);

    Console.WriteLine($"Invalid SKUs Detected:");
    var result = 0L;
    foreach (var sku in invalidSkus)
    {
        Console.WriteLine($"- {sku}");
        result += sku;
    }
    Console.WriteLine($"Sum of invalid SKUs: {result}");*/
    
    var idRanges = File.ReadAllLines(inputPath).First();

    var rangesP2 = SkuValidatorP2.GetRanges(idRanges);
    var resultP2 = SkuValidatorP2.GetInvalidSkuSum(rangesP2);

    Console.WriteLine($"Sum of invalid SKUs: {resultP2}");

}
catch (FileNotFoundException e)
{
    Console.WriteLine($"File not found: {e.Message}");
}
catch (IOException e)
{
    Console.WriteLine($"An I/O error occurred: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"An unexpected error occurred: {e.Message}");
}

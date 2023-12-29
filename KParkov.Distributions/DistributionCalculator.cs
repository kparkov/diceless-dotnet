using System.Numerics;

namespace KParkov.Distributions;

public static class DistributionCalculator
{
    /// <summary>
    /// Generate a distribution for a pool of dice going from 1 - (sides), and
    /// with a constant added.
    /// </summary>
    /// <param name="sides">The array of dice sides</param>
    /// <param name="constant">The constant to add</param>
    /// <returns>The resulting distribution</returns>
    public static Distribution GenerateDistribution(int[] sides, int constant)
    {
        // todo: why do we need the base distribution? Can we drop it?
        var baseDistribution = new List<PermutationCount> { new() { Value = 0, Permutations = 1, Ratio = 0, AtLeast = 1, AtMost = 1 } };
        BigInteger permutations = sides.Aggregate((BigInteger) 1, (p, c) => p * c);

        var cumulativeDistribution = baseDistribution;

        foreach (var s in sides)
        {
            var currentDistribution = new List<PermutationCount>();

            var cumulativeMinValue = cumulativeDistribution.Min(c => c.Value);
            var lengthOfCumulative = cumulativeDistribution.Count;
            BigInteger runningOccurrences = 0;

            for (int i = cumulativeMinValue + 1; i < cumulativeMinValue + lengthOfCumulative + s; i++)
            {
                var firstValueFromCumulative = i - s;
                var lastValueFromCumulative = i - 1;

                var sliceOfCumulativeDistribution = cumulativeDistribution.Where(item => item.Value >= firstValueFromCumulative && item.Value <= lastValueFromCumulative).ToList();
                var sumOfSlice = sliceOfCumulativeDistribution
                    .Select(x => x.Permutations)
                    .Sum();

                currentDistribution.Add(new PermutationCount
                {
                    Value = i,
                    Permutations = sumOfSlice,
                    Ratio = (double) sumOfSlice / (double) permutations,
                    AtLeast = permutations - runningOccurrences,
                    AtMost = runningOccurrences + sumOfSlice
                });
                
                runningOccurrences += sumOfSlice;
            }

            cumulativeDistribution = currentDistribution;
        }

        // todo: adding the constant after the fact seems unnecessary
        // todo: OrderBy seems unnecessary
        cumulativeDistribution = cumulativeDistribution
            .Select(count => count with { Value = count.Value + constant })
            .OrderBy(x => x.Value)
            .ToList();

        return new Distribution()
        {
            Minimum = cumulativeDistribution[0].Value,
            Maximum = cumulativeDistribution[^1].Value,
            Permutations = permutations,
            PermutationCounts = cumulativeDistribution
                .ToArray()
        };
    }
}
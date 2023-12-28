using System;
using System.Collections.Generic;
using System.Linq;

namespace KParkov.Distributions;

public static class DistributionCalculator
{
    public static Distribution GenerateDistribution(int[] _sides, int _constant)
    {
        var baseDistribution = new List<PermutationCount> { new() { Value = 0, Permutations = 1, Ratio = 0, AtLeast = 1, AtMost = 1 } };
        long permutations = _sides.Aggregate(1, (p, c) => p * c);

        var cumulativeDistribution = baseDistribution;

        foreach (var sides in _sides)
        {
            var currentDistribution = new List<PermutationCount>();

            var cumulativeMinValue = cumulativeDistribution.Min(c => c.Value);
            var lengthOfCumulative = cumulativeDistribution.Count;
            long runningOccurrences = 0;

            for (int i = cumulativeMinValue + 1; i < cumulativeMinValue + lengthOfCumulative + sides; i++)
            {
                var firstValueFromCumulative = i - sides;
                var lastValueFromCumulative = i - 1;

                var sliceOfCumulativeDistribution = cumulativeDistribution.Where(item => item.Value >= firstValueFromCumulative && item.Value <= lastValueFromCumulative).ToList();
                long sumOfSlice = sliceOfCumulativeDistribution.Sum(x => x.Permutations);

                currentDistribution.Add(new PermutationCount
                {
                    Value = i,
                    Permutations = sumOfSlice,
                    Ratio = sumOfSlice / (double) permutations,
                    AtLeast = permutations - runningOccurrences,
                    AtMost = runningOccurrences + sumOfSlice
                });
                runningOccurrences += sumOfSlice;
            }

            cumulativeDistribution = currentDistribution;
        }

        cumulativeDistribution = cumulativeDistribution
            .Select(count => count with { Value = count.Value + _constant })
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace KParkov.Distributions;

public class Distribution
{
    private List<int> _sides = new List<int>();
    private int _constant;
    private List<IPermutationCount> _permutationCounts = new List<IPermutationCount>();
    private int _permutations = 0;

    public Distribution(List<int> sides, int constant)
    {
        _sides = sides;
        _constant = constant;

        if (_sides.Count > 0)
        {
            _permutationCounts = GenerateDistribution();
        }
    }

    public IPermutationCount PermutationCountsOf(int value)
    {
        var result = _permutationCounts.Where(x => x.Value == value).ToList();

        if (result.Count == 1)
        {
            return result[0];
        }

        return new PermutationCount { Value = value, Permutations = 0, AtLeast = 0, AtMost = 0 };
    }

    public List<IPermutationCount> PermutationCounts()
    {
        return _permutationCounts.OrderBy(x => x.Value).ToList();
    }

    public int Permutations()
    {
        return _permutations;
    }

    public double Percentage(int combinationCount)
    {
        return (double)combinationCount / _permutations;
    }

    private List<IPermutationCount> GenerateDistribution()
    {
        var baseDistribution = new List<IPermutationCount> { new PermutationCount { Value = 0, Permutations = 1, AtLeast = 1, AtMost = 1 } };
        _permutations = _sides.Aggregate(1, (p, c) => p * c);

        var cumulativeDistribution = baseDistribution;

        foreach (var sides in _sides)
        {
            var currentDistribution = new List<IPermutationCount>();

            var cumulativeMinValue = cumulativeDistribution.Min(c => c.Value);
            var lengthOfCumulative = cumulativeDistribution.Count;
            var runningOccurrences = 0;

            for (int i = cumulativeMinValue + 1; i < cumulativeMinValue + lengthOfCumulative + sides; i++)
            {
                var firstValueFromCumulative = i - sides;
                var lastValueFromCumulative = i - 1;

                var sliceOfCumulativeDistribution = cumulativeDistribution.Where(item => item.Value >= firstValueFromCumulative && item.Value <= lastValueFromCumulative).ToList();
                var sumOfSlice = sliceOfCumulativeDistribution.Sum(x => x.Permutations);

                currentDistribution.Add(new PermutationCount
                {
                    Value = i,
                    Permutations = sumOfSlice,
                    AtLeast = _permutations - runningOccurrences,
                    AtMost = runningOccurrences + sumOfSlice
                });
                runningOccurrences += sumOfSlice;
            }

            cumulativeDistribution = currentDistribution;
        }

        cumulativeDistribution = cumulativeDistribution
            .Select(count =>
            {
                count.Value += _constant;
                return count;
            }).ToList();

        return cumulativeDistribution;
    }
}

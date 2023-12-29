using System.Numerics;

namespace KParkov.Diceless.Distributions;

public readonly record struct Distribution
{
    public required int Minimum { get; init; }
    public required int Maximum { get; init; }
    public required BigInteger Permutations { get; init; }
    public required PermutationCount[] PermutationCounts { get; init; }
    
    public PermutationCount PermutationCountsOf(int value) =>
        PermutationCounts
            .SingleOrDefault(x => x.Value == value, new PermutationCount
            {
                Value = value,
                Permutations = 0,
                Ratio = 0,
                AtLeast = 0,
                AtMost = 0
            });

    // public double Percentage(int combinationCount)
    // {
    //     return (double)combinationCount / Permutations;
    // }
}
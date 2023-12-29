using System.Numerics;

namespace KParkov.Distributions;

public readonly record struct PermutationCount
{
    public required int Value { get; init; }
    public required BigInteger Permutations { get; init; }
    public required double Ratio { get; init; }
    public required BigInteger AtLeast { get; init; }
    public required BigInteger AtMost { get; init; }
}
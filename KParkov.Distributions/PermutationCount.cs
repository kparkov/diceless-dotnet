namespace KParkov.Distributions;

public readonly record struct PermutationCount
{
    public required int Value { get; init; }
    public required long Permutations { get; init; }
    public required double Ratio { get; init; }
    public required long AtLeast { get; init; }
    public required long AtMost { get; init; }
}
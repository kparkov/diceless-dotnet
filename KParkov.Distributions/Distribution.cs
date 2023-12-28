namespace KParkov.Distributions;

public readonly record struct Distribution
{
    public required int Minimum { get; init; }
    public required int Maximum { get; init; }
    public required long Permutations { get; init; }
    public required PermutationCount[] PermutationCounts { get; init; }
    
    public PermutationCount PermutationCountsOf(int value)
    {
        var result = PermutationCounts
            .Where(x => x.Value == value)
            .ToArray();

        if (result.Length == 1)
        {
            return result[0];
        }

        return new PermutationCount
        {
            Value = value, 
            Permutations = 0,
            Ratio = 0,
            AtLeast = 0, 
            AtMost = 0
        };
    }
    
    // public double Percentage(int combinationCount)
    // {
    //     return (double)combinationCount / Permutations;
    // }
}
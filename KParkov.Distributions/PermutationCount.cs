namespace KParkov.Distributions;

public class PermutationCount : IPermutationCount
{
    public int Value { get; set; }
    public int Permutations { get; set; }
    public int AtLeast { get; set; }
    public int AtMost { get; set; }
}
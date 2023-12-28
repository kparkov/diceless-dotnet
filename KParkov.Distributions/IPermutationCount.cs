namespace KParkov.Distributions;

public interface IPermutationCount
{
    int Value { get; set; }
    int Permutations { get; set; }
    int AtLeast { get; set; }
    int AtMost { get; set; }
}
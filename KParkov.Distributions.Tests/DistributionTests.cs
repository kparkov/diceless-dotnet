namespace KParkov.Distributions.Tests;

using Xunit;

public class DistributionTests
{
    [Fact]
    public void ShouldCorrectlyAssess2d6()
    {
        var distribution = new Distribution(new List<int> { 6, 6 }, 0);
        Assert.Equal(11, distribution.PermutationCounts().Count);
        Assert.Equal(6, distribution.PermutationCountsOf(7).Permutations);
        Assert.Equal(1, distribution.PermutationCountsOf(2).Permutations);
    }

    [Fact]
    public void ShouldCorrectlyAssess2d6Plus1d8()
    {
        var distribution = new Distribution(new List<int> { 6, 6, 8 }, 0);
        Assert.Equal(18, distribution.PermutationCounts().Count);
        Assert.Equal(32, distribution.PermutationCountsOf(11).Permutations);
        Assert.Equal(32, distribution.PermutationCountsOf(12).Permutations);
        Assert.Equal(1, distribution.PermutationCountsOf(20).Permutations);
        Assert.Equal(0.1111111111111111, distribution.Percentage(distribution.PermutationCountsOf(12).Permutations), 15);
    }

    [Fact]
    public void ShouldCorrectlyAssess2d6Plus1d8Plus4()
    {
        var distribution = new Distribution(new List<int> { 6, 6, 8 }, 3);
        Assert.Equal(18, distribution.PermutationCounts().Count);
        Assert.Equal(32, distribution.PermutationCountsOf(14).Permutations);
        Assert.Equal(32, distribution.PermutationCountsOf(15).Permutations);
        Assert.Equal(1, distribution.PermutationCountsOf(23).Permutations);
        Assert.Equal(0.1111111111111111, distribution.Percentage(distribution.PermutationCountsOf(15).Permutations), 15);
    }

    [Fact]
    public void ProbabilityOfVeryUnlikelyShouldNeverBeRoundedTo100()
    {
        var sides = new List<int>();
        for (int i = 0; i < 40; i++)
        {
            sides.Add(6);
        }

        var distribution = new Distribution(sides, 0);
        Assert.True(distribution.Percentage(distribution.PermutationCountsOf(70).AtLeast) < 1);
    }
}

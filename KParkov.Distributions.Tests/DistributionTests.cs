using System.Numerics;
using FluentAssertions;

namespace KParkov.Distributions.Tests;

using Xunit;

public class DistributionTests
{
    [Fact]
    public void ShouldCorrectlyAssess2d6()
    {
        var distribution = DistributionCalculator.GenerateDistribution(new int[] { 6, 6 }, 0);
        Assert.Equal(11, distribution.PermutationCounts.Length);
        Assert.Equal(6, distribution.PermutationCountsOf(7).Permutations);
        Assert.Equal(1, distribution.PermutationCountsOf(2).Permutations);
    }

    [Fact]
    public void ShouldCorrectlyAssess2d6Plus1d8()
    {
        var distribution = DistributionCalculator.GenerateDistribution(new int[] { 6, 6, 8 }, 0);
        Assert.Equal(18, distribution.PermutationCounts.Length);
        Assert.Equal(32, distribution.PermutationCountsOf(11).Permutations);
        Assert.Equal(32, distribution.PermutationCountsOf(12).Permutations);
        Assert.Equal(1, distribution.PermutationCountsOf(20).Permutations);
        Assert.Equal(0.1111111111111111, distribution.PermutationCountsOf(12).Ratio, 15);
    }

    [Fact]
    public void ShouldCorrectlyAssess2d6Plus1d8Plus4()
    {
        var distribution = DistributionCalculator.GenerateDistribution(new int[] { 6, 6, 8 }, 3);
        Assert.Equal(18, distribution.PermutationCounts.Length);
        Assert.Equal(32, distribution.PermutationCountsOf(14).Permutations);
        Assert.Equal(32, distribution.PermutationCountsOf(15).Permutations);
        Assert.Equal(1, distribution.PermutationCountsOf(23).Permutations);
        Assert.Equal(0.1111111111111111, distribution.PermutationCountsOf(15).Ratio, 15);
    }

    [Theory()]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(40)]
    [InlineData(80)]
    [InlineData(160)]
    [InlineData(320)]
    public void ProbabilityOfVeryUnlikelyShouldNeverBeRoundedTo100(int numDice)
    {
        var sides = new List<int>();
        for (int i = 0; i < numDice; i++)
        {
            sides.Add(6);
        }
        
        Func<Distribution> act = () => DistributionCalculator.GenerateDistribution(sides.ToArray(), 0);

        act.Should().NotThrow();

        var distribution = act();

        PermutationCount countof70 = distribution.PermutationCountsOf(11);
        BigInteger atleast = countof70.AtLeast;
        
        atleast.DivideBy(distribution.Permutations)
            .Should()
            .BeInRange(0.0, 1.0);

        distribution.PermutationCounts.Should().BeInAscendingOrder(x => x.Value);
        distribution.PermutationCounts.Should().AllSatisfy(x =>
        {
            x.Permutations.Should().BeGreaterOrEqualTo(0);
            x.Ratio.Should().BeInRange(0, 1);
            x.AtLeast.Should().BeGreaterOrEqualTo(0);
            x.AtMost.Should().BeGreaterOrEqualTo(0);
        });
    }
}
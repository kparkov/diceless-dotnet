using FluentAssertions;
using KParkov.Diceless.Dice;

namespace KParkov.Diceless.Dice.Tests;

public class DicePoolTests
{
    [Fact]
    public void CanConstructDicePool()
    {
        var f = Dicer.SystemSeed(0);

        var pool = 
            f.Pool()
            + f.D(6)
            + f.D(8);

        pool.NumberOfDice.Should().Be(2);
    }

    [Fact]
    public void CanRollPool()
    {
        var f = Dicer.NonRandom;

        var pool = f.Pool() + f.D(6) + f.D(6) + f.D(6);

        pool.Roll();
        pool.Sum().Should().Be(10);
        pool.NumericalValues.Should().BeEquivalentTo(new[] { 2, 4, 4 });
    }
}
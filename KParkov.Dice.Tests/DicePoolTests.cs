using FluentAssertions;

namespace KParkov.Dice.Tests;

public class DicePoolTests
{
    [Fact]
    public void CanConstructDicePool()
    {
        var f = DiceFactory.SystemSeed(0);

        var pool = 
            f.Pool()
            + f.D(6)
            + f.D(8);

        pool.NumberOfDice.Should().Be(2);
    }
}
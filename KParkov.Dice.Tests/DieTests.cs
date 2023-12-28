using FluentAssertions;
using KParkov.Dice.Dice;

namespace KParkov.Dice.Tests;

public class DieTests
{
    [Fact]
    public void CanCreateD6()
    {
        Die d = DiceFactory.SystemSeed(0).D(6);
        d.NumberOfSides.Should().Be(6);
    }

    [Fact]
    public void CanRollD6()
    {
        Die d = DiceFactory.SystemSeed(0).D(6);

        d.NumericalValue.Should().Be(5);
        d.Roll();
        d.NumericalValue.Should().Be(5);
        d.Roll();
        d.NumericalValue.Should().Be(5);
        d.Roll();
        d.NumericalValue.Should().Be(4);
        d.Roll();
        d.NumericalValue.Should().Be(2);
    }
}
using FluentAssertions;
using KParkov.Diceless.Dice;
using KParkov.Diceless.Dice.Dice;

namespace KParkov.Diceless.Dice.Tests;

public class DieTests
{
    [Fact]
    public void CanCreateD6()
    {
        Die d = Dicer.NonRandom.D(6);
        d.NumberOfSides.Should().Be(6);
    }

    [Fact]
    public void CanRollD6()
    {
        Die d = Dicer.NonRandom.D(6);

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

    [Fact]
    public void CanRollD100()
    {
        Die d = Dicer.NonRandom.D(100);

        d.NumericalValue.Should().Be(73);
        d.Roll().NumericalValue.Should().Be(82);

        d.Roll().NumericalValue.Should().Be(77);
        d.NumericalValue.Should().Be(77);
        d.NumericalValue.Should().Be(77);

        d.Roll().NumericalValue.Should().Be(56);
    }
}
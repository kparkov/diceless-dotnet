using FluentAssertions;

namespace KParkov.Dice.Tests;

public class RandomizerTests
{
    private IRandomizer _randomizer;

    public RandomizerTests()
    {
        _randomizer = new SystemRandomizer(new Random(0));
    }
    
    [Fact]
    public void TestRange()
    {
        var d6 = new DiceFactory(_randomizer).D(6);

        var values = Enumerable
            .Range(0, 1000)
            .Select(x => d6.Roll().NumericalValue)
            .ToArray();

        for (var i = 1; i <= 6; i++)
        {
            values.Should().Contain(i);
        }

        values.Should().NotContain(x => x < 1 || x > 6);
    } 
}
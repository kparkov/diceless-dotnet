using KParkov.Dice.Dice;

namespace KParkov.Dice;

public sealed class DiceFactory
{
    private readonly IRandomizer _randomizer;

    public DiceFactory(IRandomizer randomizer)
    {
        _randomizer = randomizer;
    }

    public DicePool Pool() => new(_randomizer);
    public Die D(int numberOfSides) => Die.D(_randomizer, numberOfSides);

    
    public static DiceFactory System => new(new SystemRandomizer(new Random()));
    public static DiceFactory SystemSeed(int seed) => new(new SystemRandomizer(new Random(seed)));
}
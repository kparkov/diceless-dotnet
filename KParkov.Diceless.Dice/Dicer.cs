using KParkov.Diceless.Dice.Dice;

namespace KParkov.Diceless.Dice;

public sealed class Dicer
{
    private readonly IRandomizer _randomizer;

    public Dicer(IRandomizer randomizer)
    {
        _randomizer = randomizer;
    }

    public DicePool Pool() => new(_randomizer);
    public Die D(int numberOfSides) => Die.D(_randomizer, numberOfSides);

    
    public static Dicer System => new(new SystemRandomizer(new Random()));
    public static Dicer SystemSeed(int seed) => new(new SystemRandomizer(new Random(seed)));
    public static Dicer NonRandom => SystemSeed(0);
}
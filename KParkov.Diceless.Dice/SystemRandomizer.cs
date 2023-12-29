namespace KParkov.Diceless.Dice;

public sealed class SystemRandomizer : IRandomizer
{
    private readonly Random _random;

    public SystemRandomizer(Random random)
    {
        _random = random;
    }

    public int NextInt(int min, int max)
    {
        return _random.Next(min, max);
    }

    public double NextDouble()
    {
        return _random.NextDouble();
    }
}
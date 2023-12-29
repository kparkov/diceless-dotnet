namespace KParkov.Diceless.Dice;

public interface IRandomizer
{
    int NextInt(int min, int max);
    double NextDouble();
}
using KParkov.Diceless.Dice.Dice;

namespace KParkov.Diceless.Dice;

public sealed class DicePool
{
    private readonly IRandomizer _randomizer;
    
    public Die[] Dice { get; } = Array.Empty<Die>();
    public int NumberOfDice => Dice.Length;
    public int[] NumericalValues => Dice.Select(x => x.NumericalValue).ToArray();

    public static DicePool operator +(DicePool a, DicePool b) => a.Add(b);
    public static DicePool operator +(DicePool a, Die b) => a.Add(b);
    
    internal DicePool(IRandomizer randomizer)
    {
        _randomizer = randomizer;
    }
    
    private DicePool(IRandomizer randomizer, Die[] dice)
    {
        _randomizer = randomizer;
        Dice = dice;
    }
    
    public DicePool Add(Die die)
    {
        return new DicePool(_randomizer, Dice.Append(die).ToArray());
    }

    public DicePool Add(DicePool pool)
    {
        return new DicePool(_randomizer, Dice.Concat(pool.Dice).ToArray());
    }

    public void Roll()
    {
        foreach (var die in Dice) die.Roll();
    }

    public int Sum()
    {
        return Dice.Sum(x => x.NumericalValue);
    }
}
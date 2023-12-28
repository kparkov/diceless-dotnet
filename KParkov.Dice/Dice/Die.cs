namespace KParkov.Dice.Dice;

public class Die
{
    private readonly IRandomizer _randomizer;
    private readonly IDieSide[] _sides;
    
    public IDieSide Value { get; private set; }
    public int NumberOfSides => _sides.Length;
    public int NumericalValue => Value.NumericalValue;
    
    private Die(IRandomizer randomizer, IDieSide[] sides)
    {
        _sides = sides;
        _randomizer = randomizer;
        if (sides.Length == 0) throw new Exception("No sides on die");
        Value = sides[0];
        Roll();
    }

    public IDieSide Roll()
    {
        var sideIndex = _randomizer.NextInt(0, _sides.Length);
        Value = _sides[sideIndex];
        return Value;
    }

    internal static Die D(IRandomizer randomizer, int numberSides)
    {
        var sides = Enumerable
            .Range(1, numberSides)
            .Select(x => new NumericalSide() { NumericalValue = x })
            .Cast<IDieSide>()
            .ToArray();
        
        return new Die(randomizer, sides);
    } 
        
}
namespace KParkov.Dice.Dice;

public record struct NumericalSide : IDieSide
{
    public int NumericalValue { get; init; }
}
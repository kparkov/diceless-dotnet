using System.Numerics;

namespace KParkov.Diceless.Distributions;

public static class BigIntegerExtensions
{
    public static BigInteger Sum(this IEnumerable<BigInteger> collection)
    {
        BigInteger tally = 0;

        foreach (var i in collection)
        {
            tally += i;
        }

        return tally;
    }

    /// <summary>
    /// Divides two BigIntegers
    /// </summary>
    /// <param name="a">This BigInteger</param>
    /// <param name="b">The BigInteger to divide by</param>
    /// <remarks>
    ///     This hack can be used for REALLY big numbers:
    ///     double result = Math.Exp(BigInteger.Log(x) - BigInteger.Log(y));
    ///
    ///     See: https://stackoverflow.com/a/11859314
    /// </remarks>
    /// <returns></returns>
    public static double DivideBy(this BigInteger a, BigInteger b)
    {
        return (double)a / (double)b;
    }
}
namespace NonDeterminismInTests.Tests.Refactored;

public static class RandomNumberGenerator
{
    private static readonly Random _random = new();

    public static int AnyInt(int min, int max) => _random.Next(min, max);

    public static int AnyInt() => AnyInt(int.MinValue, int.MaxValue);

    public static int AnyIntBiggerThan(int min) => AnyInt(min, int.MaxValue);

    public static int AnyIntBiggerOrEqualThan(int min) => AnyInt(min, int.MaxValue);

    public static int AnyIntLowerThan(int max) => AnyInt(int.MinValue, max);

    public static int AnyIntLowerOrEqualThan(int max) => AnyInt(int.MinValue, max + 1);
}
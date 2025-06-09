using NonDeterminismInTests.Logic.Naive;
using static NonDeterminismInTests.Tests.Refactored.RandomNumberGenerator;

namespace NonDeterminismInTests.Tests.Refactored;

public class UpgradePressureCalculatorTests
{
    [Theory]
    [InlineData(0, 1000, 500, 0.5)]    // Midpoint
    [InlineData(0, 1000, 0, 0.0)]      // Exactly at min
    [InlineData(0, 1000, 1000, 1.0)]   // Exactly at max
    [InlineData(100, 1000, 0, 0.0)]    // Below min, clamped
    [InlineData(0, 1000, 1500, 1.0)]   // Above max, clamped
    public void Calculates_upgrade_pressure_correctly_for_specific_cases(
        int minThreshold, int maxThreshold, int usage, decimal expected)
    {
        var calculator = new UpgradePressureCalculator(minThreshold, maxThreshold);

        var result = calculator.Calculate(usage);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(CalculateUsageTestData))]
    public void Calculates_upgrade_pressure_correctly_for_randomized_inputs(
        int minThreshold, int maxThreshold, int usage, decimal expected)
    {
        var calculator = new UpgradePressureCalculator(minThreshold, maxThreshold);

        var result = calculator.Calculate(usage);

        Assert.True(
            result == expected,
            $"Failed for minThreshold={minThreshold}, maxThreshold={maxThreshold}, usage={usage}. Expected: {expected}, Actual: {result}");
    }

    public static IEnumerable<object[]> CalculateUsageTestData()
    {
        int min = AnyInt();
        int max = AnyInt();

        int range = max - min;

        yield return new object[] { min, max, min + range / 2, 0.5m };   // Midpoint
        yield return new object[] { min, max, min, 0.0m };               // Exactly at min
        yield return new object[] { min, max, max, 1.0m };               // Exactly at max
        yield return new object[] { min, max, min - 1, 0.0m };           // Below min (clamped)
        yield return new object[] { min, max, max + 1, 1.0m };           // Above max (clamped)
    }

    [Fact]
    public void Calculate_returns_expected_value_when_usage_is_within_range()
    {
        int minThreshold = AnyInt();
        int maxThreshold = AnyInt();
        int usage = AnyInt(minThreshold, maxThreshold);

        var calculator = new UpgradePressureCalculator(minThreshold, maxThreshold);

        var result = calculator.Calculate(usage);

        Assert.InRange(result, 0.0m, 1.0m);
    }
}